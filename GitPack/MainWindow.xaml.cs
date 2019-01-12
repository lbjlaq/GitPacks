using System;
using System.Collections.Generic;
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
using System.IO;
using Microsoft.Win32;
using System.Configuration;

namespace GitPack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Int64 num = Int64.Parse(creatnum.Text);
                RandCreat.Run(filepath1.Text+ "\\" + DateTime.Now.ToFileTime() + ".txt", num);
                MessageBox.Show("创建成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            dialog.SelectedPath = Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filepath1.Text = dialog.SelectedPath ;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "confif file|*.config|any file|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string path = openFileDialog.FileName;
                    ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                    configFile.ExeConfigFilename = path;
                    Configuration appConf = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                    SCHOOL_ID school_id;

                    if ((school_id = (SCHOOL_ID)appConf.Sections["SCHOOL_ID"]) == null)
                    {
                        MessageBox.Show("无法找到id");
                        return;
                    }

                    SECRET_KEY secret_key;
                    if ((secret_key = (SECRET_KEY)appConf.Sections["SECRET_KEY"]) == null)
                    {
                        MessageBox.Show("无法找到key");
                        return;
                    }
                    School_id.Text = school_id.School_id;
                    Secret_key.Text = secret_key.secret_key;
                    MessageBox.Show("读取成功");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, "infor.config");
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                configFile.ExeConfigFilename = path;
                Configuration appConf = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                SCHOOL_ID school_id;
                SECRET_KEY secret_key;
                Initconfig(appConf, out school_id, out secret_key);
                
                appConf.Save(ConfigurationSaveMode.Full);
                MessageBox.Show("创建成功");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, "infor.config");
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();
                configFile.ExeConfigFilename = path;
                Configuration appConf = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                SCHOOL_ID school_id;
                SECRET_KEY secret_key;
                Initconfig(appConf, out school_id, out secret_key);
                school_id.School_id = School_id.Text;
                secret_key.secret_key = Secret_key.Text;
                appConf.Save(ConfigurationSaveMode.Full);
                MessageBox.Show("保存成功");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void Initconfig(Configuration appConf, out SCHOOL_ID school_id, out SECRET_KEY secret_key)
        {
            if ((school_id = (SCHOOL_ID)appConf.Sections["SCHOOL_ID"]) == null)
            {
                school_id = new SCHOOL_ID();
                appConf.Sections.Add("SCHOOL_ID", school_id);
                school_id.School_id = School_id.Text;
            }

            if ((secret_key = (SECRET_KEY)appConf.Sections["SECRET_KEY"]) == null)
            {
                secret_key = new SECRET_KEY();
                appConf.Sections.Add("SECRET_KEY", secret_key);
            }
            school_id.School_id = secret_key.secret_key = "default";
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt file|*.txt";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (openFileDialog.ShowDialog()==true)
            {
                sourcepath.Text = openFileDialog.FileName;
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (Secret_key.Text==""||School_id.Text=="")
            {
                MessageBox.Show("key or id is empty");
                return;
            }
            
            int a, b;
            try {
                Signature signature = new Signature(sourcepath.Text+"-out.txt",sourcepath.Text,Secret_key.Text,School_id.Text);
                signature.Run(out a, out b);
                MessageBox.Show(string.Format("成功{0} 失败{1}", b, a));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            GetPack pack = new GetPack("jmatson2@mcc.edu", "qq771919124");
            pack.loginAsync();
        }
    }
}
