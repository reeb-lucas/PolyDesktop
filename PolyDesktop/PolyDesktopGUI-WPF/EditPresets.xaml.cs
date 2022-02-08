using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PolyDesktopGUI_WPF
{
    /// <summary>
    /// Interaction logic for EditPresets.xaml
    /// </summary>
    public partial class EditPresets : Page
    {
        static string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string filename = System.IO.Path.Combine(localApplicationData, "Preset"); //filepath for presets with the word Prest appended to make future code easier
        private string connectionString = "server=satou.cset.oit.edu,5433; database=PolyDesktop; UID=PolyCode; password=P0lyC0d3";
        string[] bucket;
        public EditPresets()
        {
            InitializeComponent();
            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
            ThemeManager.Current.SyncTheme();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
        public Preset[] Presets { get { return GatherPresets(); } }
        public Preset[] GatherPresets() //returns all presets in an observable object for the listview to display
        {
            Preset[] container = new Preset[Directory.GetFiles(localApplicationData).Length]; //sees how many files are in the directory for presets and sets the array size
            for (int i = 0; i < 99; i++)
            {
                try
                {
                    string temp = File.ReadAllText(filename + i + ".txt");
                    string[] bucket = temp.Split(','); //used to Split(", ")
                    Preset preset = new Preset();
                    preset.Name = bucket[0];
                    preset.Mode = bucket[1];
                    preset.numComputers = Int32.Parse(bucket[2]);
                    container[i] = preset;
                    //PresetList.Header = "Presets";
                }
                catch
                {
                    if (i == 0)
                    {
                        //PresetList.Header = "No Presets Found";
                    }
                    break;
                }
            }
            return container;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) //when preset is selected, display all preset info from file and populate computertable
        {
            try
            {
                string temp = File.ReadAllText(filename + PresetList.SelectedIndex + ".txt");
                bucket = temp.Split(',');
                NameBox.Text = bucket[0];
                ModeButton.Content = bucket[1];
                ComputerTable.ItemsSource = Computers;
                int index = 0;
                for (int i = 2; i < bucket.Length; i += 2)
                {
                    if (bucket[i] != "")
                    {
                        index++;
                    }
                }
                NumBlock.Text = (index - 1).ToString();

            }
            catch { }
        }
        private void ComputerTable_SelectionChanged(object sender, SelectionChangedEventArgs e) //ListView object holding all computers in a preset
        {
            if (ComputerTable.SelectedItem != null)
            {
                int index = 1;
                for (int i = 0; i < ComputerTable.SelectedIndex + 1; i++)
                {
                    index += 2;
                }
                if (bucket[index] != null)
                {
                    FlyoutIDBlock.Text = bucket[index];
                    FlyoutNameBlock.Text = ExecuteQuery(index);
                    FlyoutNicknameBox.Text = bucket[index + 1];
                }
                OpenSingleFlyout(ComputerFlyout); //flyout with computer info and oportunity to change nickname
            }
        }
        public Computer[] Computers { get { return GatherComputers(); } }
        public Computer[] GatherComputers() //returns all computers in a preset in an observable array to populate listview
        {
            try
            {
                string temp = File.ReadAllText(filename + PresetList.SelectedIndex + ".txt");
                bucket = temp.Split(',');
                Computer[] container = new Computer[Int32.Parse(bucket[2])];
                try
                {
                    int j = 1;
                    for (int i = 0; i < 99; i++)
                    {
                        j += 2;
                        Computer preset = new Computer();
                        if (bucket[j] != null)
                        {
                            preset.ID = bucket[j];
                            preset.Name = ExecuteQuery(j);
                            if (bucket[j + 1] != null)
                            {
                                preset.Nickname = bucket[j + 1];
                            }
                            else
                            {
                                preset.Nickname = preset.Name;
                            }
                        }
                        container[i] = preset;
                    }
                }
                catch
                {
                    return container;
                }
            }
            catch { }
            return new Computer[0];
        }
        public Computer[] AllComputers { get { return GatherAllComputers(); } }
        public Computer[] GatherAllComputers(string searchTerm = null) //returns up to 5 computers in an observable array to populate listview
        {
            Computer[] container = new Computer[5];

            using (var connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT c_ID, c_name FROM PolyDesktop.dbo.desktop";
                if (searchTerm != null)
                {
                    sql = "SELECT c_ID, c_name FROM PolyDesktop.dbo.desktop WHERE c_name LIKE'%" + searchTerm + "%' OR c_ID LIKE '%" + searchTerm + "%'";
                }
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < 5 && reader.Read(); i++) //only returns a max of 15 results
                        {
                            Computer temp = new Computer();
                            temp.ID = reader.GetInt32(0).ToString();
                            temp.Name = reader.GetString(1); //UUUUUUHHHHHHH, I can't get more than the first row
                            temp.Nickname = temp.Name;
                            container[i] = temp;
                        }
                        reader.Close();
                    }
                }
            }
            return container;
        }
        private string ExecuteQuery(int index) //fetch computer name given c_ID
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT c_name FROM PolyDesktop.dbo.desktop WHERE c_ID = " + bucket[index];
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        string name = "No Name Found";
                        if (reader.HasRows)
                        {
                            reader.Read();
                            name = reader.GetString(0);
                            reader.Close();
                        }
                        return name;
                    }
                }
            }
        }
        private void NicknameChangeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 2;
            for (int i = 0; i < ComputerTable.SelectedIndex + 1; i++)
            {
                index += 2;
            }
            if (FlyoutNicknameBox.Text == null)
            {
                bucket[index] = bucket[index - 1];

            }
            else if (bucket[index] != null)
            {
                bucket[index] = NormalizeInput(FlyoutNicknameBox.Text);
            }
        }
        private void PresetSaveButton_Click(object sender, RoutedEventArgs e) //write back to file using bucket object
        {
            SavePreset();
        }
        private void SavePreset()
        {
            if (PresetList.SelectedIndex != -1)
            {
                string saveString = NormalizeInput(NameBox.Text) + "," + NormalizeInput(ModeButton.Content.ToString()) + "," + NormalizeInput(NumBlock.Text);
                for (int i = 3; i < bucket.Length; i++)
                {
                    saveString = saveString + "," + bucket[i];
                }
                if (saveString != null)
                {
                    File.WriteAllText(filename + PresetList.SelectedIndex + ".txt", saveString);
                    ComputerTable.ItemsSource = Computers;
                }
            }
            PresetList.ItemsSource = Presets;
        }
        private void AddComputerButton_Click(object sender, RoutedEventArgs e) //popup to add computer from list to bucket and set nickname
        {
            //FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            OpenSingleFlyout(AddComputerFlyout);
            SearchListBox.ItemsSource = GatherAllComputers();

        }
        private void RemoveComputerButton_Click(object sender, RoutedEventArgs e) //remove computer from preset
        {
            int numComputers = Convert.ToInt32(bucket[2]);
            for (int i = (ComputerTable.SelectedIndex * 2) + 3; i < (numComputers * 2) + 1; i++) //shift up following computers
            {
                string a = bucket[i];
                string b = bucket[i + 2];
                bucket[i] = bucket[i + 2];
            }
            bucket = bucket.Take(bucket.Length - 2).ToArray(); //remove last computer
            bucket[2] = (numComputers - 1).ToString();
            string temp = string.Join(",", bucket);
            bucket = temp.Split(',');
            File.WriteAllText(filename + PresetList.SelectedIndex + ".txt", temp);
            ComputerTable.ItemsSource = Computers;
        }
        private void TestButton_Click(object sender, RoutedEventArgs e) //this fills the computer with test presets
        {
            File.WriteAllText(filename + 0 + ".txt", "TestPreset1,Tab,3,14252351,TestNickname 0,162,TestNickname 1,158964,TestNickname 2");
            File.WriteAllText(filename + 1 + ".txt", "TestPreset2,Group,6,14252351,TestNickname 0,213286983,TestNickname 1,158964,TestNickname 2,162,TestNickname 3,102538501,TestNickname 4,25389172,TestNickname 5");
            File.WriteAllText(filename + 2 + ".txt", "TestPreset3,Basic,4,14252351,TestNickname 0,162,TestNickname 1,158964,TestNickname 2,213286983,TestNickname 3");
            File.WriteAllText(filename + 3 + ".txt", "TestPreset4,Overlay,5,14252351,TestNickname 0,162,TestNickname 1,158964,TestNickname 2,213286983,TestNickname 3,102538501,TestNickname 4");
            PresetList.ItemsSource = Presets;
        }
        private void ModeButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSingleFlyout(ModePickerFlyout);
        }
        private void BasicButton_Click(object sender, RoutedEventArgs e)
        {
            ModeButton.Content = "Basic";
        }
        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            ModeButton.Content = "Tab";
        }
        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            ModeButton.Content = "Group";
        }
        private string NormalizeInput(string input) //remove commas from input to make sure file is structured correctly
        {
            return input.Replace(",", "");
        }
        private void OpenSingleFlyout(MahApps.Metro.Controls.Flyout flyout)
        {
            ModePickerFlyout.IsOpen = false;
            ComputerFlyout.IsOpen = false;
            AddComputerFlyout.IsOpen = false;
            flyout.IsOpen = true;
        }
        private void DeletePresetButton_Click(object sender, RoutedEventArgs e) //pop-up to confirm deletion
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                File.Delete(filename + PresetList.SelectedIndex + ".txt");
                for (int i = PresetList.SelectedIndex + 1; i < 100; i++)
                {
                    try
                    {
                        System.IO.File.Move(filename + i + ".txt", filename + (i - 1) + ".txt");
                    }
                    catch { }
                }
                PresetList.ItemsSource = Presets;
            }
        }
        private void search_QueryChanged(object sender, TextChangedEventArgs e)
        {
            SearchListBox.ItemsSource = GatherAllComputers(SearchBox.Text);
        }
        private void SearchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //Adding computer to preset with default nickname being the computer name
        {
            if (SearchListBox.SelectedItem != null && PresetList.HasItems)
            {
                bool alreadyExists = false;
                Computer temp = (Computer)SearchListBox.SelectedItem;
                for (int i = 0; i < Convert.ToInt32(bucket[2]); i++)
                {
                    i++; //add another to skip over computer names and just check ID
                    if (bucket[i + 2] == temp.ID)
                    {
                        alreadyExists = true;
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Selected computer already in preset.", "Add Computer Error", System.Windows.MessageBoxButton.OK);                        
                        break;
                    }
                }

                if (!alreadyExists)
                {
                    bucket[2] = (Convert.ToInt32(bucket[2]) + 1).ToString();
                    string tempBucket = string.Join(",", bucket);
                    tempBucket = tempBucket + "," + temp.ID + "," + temp.Name;
                    bucket = tempBucket.Split(',');
                    File.WriteAllText(filename + PresetList.SelectedIndex + ".txt", tempBucket);
                    ComputerTable.ItemsSource = Computers;
                    AddComputerFlyout.IsOpen = false;
                }
            }
        }
    }
    public class Preset
    {
        public string Name { get; set; }
        public string Mode { get; set; }
        public int numComputers { get; set; }
    }
    public class Computer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public bool isFull { get; set; } = false;
    }
}

