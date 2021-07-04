using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Medico
{
    /// <summary>
    /// Interaction logic for Log_In.xaml
    /// </summary>
    public partial class Log_In : Window
    {
        public Log_In()
        {
           
                InitializeComponent();
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
           

        }
        DB db = new DB();
  

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    loginnow();
                }
            }catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                loginnow();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        void loginnow()
        {
            DataTable s = db.RunReader("select id,name,password,[UserType].type_name from [USER] , [UserType] WHERE [USER].type=[UserType].type_code and NAME = '" + txtEName.Text + "'");
            if (s.Rows.Count <= 0)
                MessageBox.Show("اسم الامستخدم غير صحيح");
            else
            {
                if (s.Rows[0][2].ToString() != passbox.Password.ToString())
                    MessageBox.Show("كلمة مرور غير صحيحة");
                else
                {
                    if (s.Rows[0][3].ToString() == "admin")
                    {
                        adminForm aa = new adminForm();
                        aa.ShowDialog();
                    }
                    else if (s.Rows[0][3].ToString() == "receptionist")
                    {
                        Receptionist aa = new Receptionist();
                        aa.ShowDialog();
                    }
                    else if (s.Rows[0][3].ToString() == "doctor")
                    {
                        docscreen aa = new docscreen();
                        aa.ShowDialog();
                    }
                    else if (s.Rows[0][3].ToString() == "pharmacist")
                    {
                        pharmacist aa = new pharmacist();
                        aa.ShowDialog(); 
                    }
                    else if (s.Rows[0][3].ToString() == "lab")
                    {
                        labs aa = new labs();
                        aa.ShowDialog();
                    }
                    else if (s.Rows[0][3].ToString() == "rays")
                    {
                        rays aa = new rays();
                        aa.ShowDialog();
                    }
                    this.Close();
                }

            }
        }
    }
}
