
using ConfigPrototyp.Class;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Forms;
using ClassFiles;
using ClassFiles.Services;
using ClassFiles.Classes;
using System.Windows.Controls;
using DataGrid = System.Windows.Controls.DataGrid;
using System;
using System.Reflection;
using System.IO;
using System.Windows.Documents;
using System.Linq;
using ClassesDuplikate.Services;

namespace ConfigPrototyp
{
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// This is the main window
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        ///


        bool check = true;

       

        public MainWindow()
        {
            this.InitializeComponent();


            UebersichtFiles.MaxColumnWidth = 600;
            UebersichtFiles.MinColumnWidth = 40;
            UebersichtFiles.ColumnHeaderHeight = 35;

          
        }

        /// <summary>
        /// Overwrite the config file. 
        /// Saving the new changes in the config file.
        /// Combibox getting items after update.
        /// </summary>
        /// <param name="sender">information variable</param>
        /// <param name="e">event variable</param>
        private void UpdateSettings(object sender, RoutedEventArgs e)
        {
            // string array for the combibox
            string[] sA;
            string filetype = fileTyp.Text;
            string filepath = dataPath.Text;

            WpfMethods c = new WpfMethods();
            //update the Config File
            c.Update(filetype, filepath);
            //Refresh the Combiboxitems
            c.CombiBoxRefresh(out sA);
            //save the Refreshed Combibox in the combibox
            cBox.ItemsSource = sA;
            
            fileTyp.Text = "";
        }




        /// <summary>
        /// When the window is loaded, get the information from the config file.
        /// the datapath load the config data as input.
        /// </summary>
        /// <param name="sender">information variable</param>
        /// <param name="e">event variable</param>
        private void FileType_Loaded(object sender, RoutedEventArgs e)
        {
            var configStr = ConfigurationManager.AppSettings;
            // reading the key and values Bin the config file
            string[] sA = new string[configStr.Count];
            WpfMethods c = new WpfMethods();
            //Refresh the Combiboxitems
            c.CombiBoxRefresh(out sA);
            //save the Refreshed Combibox in the combibox
            cBox.ItemsSource = sA;
            cBox.SelectedIndex = 1;
            // variable to open the config file 
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            dataPath.Text = configFile.AppSettings.Settings["filePath"].Value;
        }

        /// <summary>
        /// The use can open the directory path with this method.
        /// Saves the selected Datapath in the configfile
        /// </summary>
        /// <param name="sender">information variable</param>
        /// <param name="e">event variable</param>
        private void OpenFileDir(object sender, RoutedEventArgs e)
        {
            //Create an Instance  of FolderBrowserDialog
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            //Open the File Dialog
            openFileDialog.ShowDialog();
            WpfMethods c = new WpfMethods();
            //Change the Datapath in the Config file
            c.ChangeDatapath(openFileDialog.SelectedPath);
            //The slected datapath is used as input for the textbox
            dataPath.Text = openFileDialog.SelectedPath;
        }
        /// <summary>
        /// Selecting the Combobox item and and load it in the textbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selected(object sender, RoutedEventArgs e)
        {
            TbFiletype.Text = cBox.SelectedItem.ToString();
        }
        /// <summary>
        /// Starting the whole Programm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start(object sender, RoutedEventArgs e)
        {
            //string filetype = TbFiletype.Text;
            string filetype = "*." + TbFiletype.Text;
            string filepath = dataPath.Text;

            // Create Ignore Files
            string ignorefile = IgnoreFile.CreateIgnoreFile(filepath);
            string ignorePathsFile = IgnorePaths.CreateIgnorePathFile(filepath);

            // Get all files in the directory
            GetFileList.GetFileNames(filepath, filetype, out List<string> fileList);
            FilesAdd.Files(fileList, ignorefile, out List<FilesRead> files);

            DuplicateCheck.DoubleCheck(files, out Dictionary<string, Output> output);
            List<WritetoGrid> final = new List<WritetoGrid>();


            foreach (KeyValuePair<string, Output> variable in output)
            {
                string filenames = "";
                int point = filepath.Length;


                for (int i = 0; i < variable.Value.FileName.Count; i++)
                {
                    string shortname = variable.Value.FileName[i].DirectoryName;
                    shortname = shortname.Remove(0, point);
                    if (filenames != variable.Key)
                    {
                        final.Add(new WritetoGrid() { duplicate = variable.Key, duplicatefun = variable.Key, duplicatequantity = Convert.ToString(variable.Value.Duplicatenumber), fileNames = shortname + @"\" + variable.Value.FileName[i].Name, linenumber = Convert.ToString(variable.Value.LineNumber[i]) });
                        filenames = variable.Key;
                    }
                    else
                    {
                        final.Add(new WritetoGrid() { duplicate = "", duplicatefun = variable.Key, duplicatequantity = "", fileNames = shortname + @"\" + variable.Value.FileName[i].Name, linenumber = Convert.ToString(variable.Value.LineNumber[i]) });
                    }
                }
                // Put Dictionary Data to List of WritetoGrid class
            }

            UebersichtFiles.ItemsSource = final;
        }

        private void hideandshow_Click(object sender, RoutedEventArgs e)
        {
            if (check)
            {
               
                Wrap.Visibility = Visibility.Visible;
                check = false;
            }
            else
            {
                Wrap.Visibility = Visibility.Collapsed;
                check = true;

            }

        }

        private void Wrap_Loaded(object sender, RoutedEventArgs e)
        {
            Wrap.Visibility = Visibility.Collapsed;
        }

        private void UebersichtFiles_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            final.Children.Clear();
            string propertyValue = "";
            string duplicateValue = "";
            
            DataGrid dg = sender as DataGrid;
            DataGridCellInfo cell = dg.CurrentCell;
            object cellContent = cell.Item;

            if (cellContent != null)
            {
                dynamic dynamicObject = cellContent;
                propertyValue = dynamicObject.fileNames;
                duplicateValue = dynamicObject.duplicatefun;

            }

            List<string> a = new List<string>();
            foreach (DataGridCellInfo cells in dg.SelectedCells)
            {
                a.Add(((ConfigPrototyp.Class.WritetoGrid)cells.Item).fileNames);
            }

            List<string> noDupes = a.Distinct().ToList();

            string[] split = noDupes.ToArray();

            

            string text = string.Empty;

            //string[] updatedSplitwithoutlastrow = new string[split.Length - 1];

            
            System.Drawing.SolidBrush drawingBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Windows.Media.Brush mediaBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(drawingBrush.Color.A, drawingBrush.Color.R, drawingBrush.Color.G, drawingBrush.Color.B));

            System.Drawing.SolidBrush drawingBrushs = new System.Drawing.SolidBrush(System.Drawing.Color.NavajoWhite);
            System.Windows.Media.Brush mediaBrushs = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(drawingBrushs.Color.A, drawingBrushs.Color.R, drawingBrushs.Color.G, drawingBrushs.Color.B));

            //for (int i = 0; i < updatedSplitwithoutlastrow.Length; i++)
            //{
            //    updatedSplitwithoutlastrow[i] = split[i];
            //}

            int counter = 0;

            for (int i = 0; i < split.Length; i++)
            {

                //text = File.ReadAllText(updatedSplitwithoutlastrow[i]);
                text = dataPath.Text;
                string test;
                test = split[i];
                text = text + test;
                text = File.ReadAllText(text);

                StackPanel stack = new StackPanel();

                stack.Margin = new Thickness(5, 0, 5, 0);
              
                Grid.SetColumn(stack, counter);
                final.Children.Add(stack);

                System.Windows.Controls.TextBox header = new System.Windows.Controls.TextBox();
                //header.Text = updatedSplitwithoutlastrow[i] + "\n";
                header.BorderThickness = new Thickness(0,0,0,0);
                header.FontWeight = FontWeights.Bold;
                
                header.FontSize = 11;
                stack.Children.Add(header);
                             

                string[] splitsec = text.Split('\n', '\r');

                List<string> finalstring = new List<string>();
                
                for (int f = 0; f < splitsec.Length; f++)
                {
                    if (splitsec[f] != string.Empty)
                    {
                        finalstring.Add(splitsec[f].ToLower());
                    }
                }

                //for (int g = 0; g < finalstring.Count; g++)
                //{
                //    if (finalstring[g].Contains(duplicateValue))
                //    {
                //        if (finalstring[0].Contains(duplicateValue))
                //        {
                //            finalstring.RemoveRange(0, g);
                //            break;
                //        }
                //        else
                //        {
                //            finalstring.RemoveRange(0, g - 1);
                //            break;
                //        }
                //    }                 
                //}

                //int length = finalstring.Count;

                //for (int z = 0; z < finalstring.Count; z++)
                //{
                //    if (finalstring[z].Contains(duplicateValue))
                //    {

                //        if (finalstring[length - 1].Contains(duplicateValue))
                //        {
                //            break;
                //        }
                //        else
                //        {
                //            finalstring.RemoveRange(z + 1 ,finalstring.Count - z - 2);
                //        }
                //    }
                //}


                string[] updatedArray = new string[finalstring.Count];
                updatedArray = finalstring.ToArray();


                for (int k = 0; k < updatedArray.Length; k++)
                {
                    //erstelle Texbox
                    System.Windows.Controls.TextBox textbox = new System.Windows.Controls.TextBox();
                    textbox.TextWrapping = TextWrapping.Wrap;
                    textbox.BorderThickness = new Thickness(0);
                    textbox.IsReadOnly = true;

                    if (updatedArray[k].Contains(duplicateValue))
                    {
                        textbox.Foreground = mediaBrush;
                        textbox.Text = updatedArray[k];
                        stack.Children.Add(textbox);
                    }
                    else
                    {
                        textbox.Text = updatedArray[k];
                        stack.Children.Add(textbox);

                    }
                }

                counter++;

            }
        }

        private void gotoignorefile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gotoignorePath_Click(object sender, RoutedEventArgs e)
        {

        }
    } 
}
