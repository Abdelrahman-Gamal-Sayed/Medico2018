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
using System.Windows.Shapes;
using System.Data;
using System.Globalization;
using System.Threading;

namespace Medico
{
    /// <summary>
    /// Interaction logic for Receptionist.xaml
    /// </summary>
    public partial class Receptionist : Window
    {
        public Receptionist()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            get_reserves();
            gover = db.RunReader("select * from [Governorate] order by [Governorate_CODE]");
            patientCitytxt.ItemsSource = gover.DefaultView;
        }
        DB db = new DB();
        DataTable gover;
        private void newPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            patientCodetxt.Text = "";
            patientAnametxt.Text = "";
            patientEnametxt.Text = "";
            patientIDtxt.Text = "";
            patientCitytxt.Text = "";
            patientAreatxt.Text = "";
            patientBDtxt.Text = "";
            patientPhonetxt.Text = "";
            maleRB.IsChecked = false;
            femaleRB.IsChecked = false;
            get_reserves();
        }
        DateTime ss;

        private void get_reserves()
        {
            string query = @"select r.serial,p.aname,p.ename
                        from patient p
                        join reserve r
                        on r.Patient_id =p.id";
            DataTable reserveDT = db.RunReader(query);
            reserveDT.Columns[0].ColumnName = "رقم الحجز";
            reserveDT.Columns[1].ColumnName = "اسم المريض";
            reserveDT.Columns[2].ColumnName = "اسم المريض بالانجليزي";
            reserveGrid.ItemsSource = reserveDT.DefaultView;

        }
        private void savePatientBtn_Click(object sender, RoutedEventArgs e)
        {

            if (patientIDtxt.Text.Length != 14)
            {
                MessageBox.Show("من فضلك ادخل 14 رقم للبطاقة");
            }
            else
            {
                String gender = "";
                if (maleRB.IsChecked == true)
                {
                    gender = "male";
                }

                else if (femaleRB.IsChecked == true)
                {
                    gender = "female";
                }

                string ssn;
                if (patientIDtxt.Text == "")
                    ssn = null;
                else
                    ssn = patientIDtxt.Text;
                
                CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                Thread.CurrentThread.CurrentCulture = ci;
                string da;
                if (patientBDtxt.Text != "")
                {
                   da = patientBDtxt.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    da = null;
                }

                DataTable temp = db.RunReader("select * from [Patient] where [ssn]='" + patientIDtxt.Text + "' and [ename]='" + patientEnametxt.Text + "'  ");
                if (temp.Rows.Count > 0) { }
                else
                {
                    db.RunNonQuery(@"insert into [Patient]  ([ssn],[ename],[aname],[gender],[phone],[DateOfBirth],[city],[area]) " +
                    "values ('" + ssn + "','" + patientEnametxt.Text + "',N'" + patientAnametxt.Text + "','" + gender + "','" + patientPhonetxt.Text + "','" + da + "',N'" + patientCitytxt.Text + "',N'" + patientAreatxt.Text + "')", "تم الحفظ بنجاح");
                    patientCodetxt.Text = db.RunReader("select [Id] from [Patient] where [ssn]='" + patientIDtxt.Text + "' and [ename]='" + patientEnametxt.Text + "' and [phone]='" + patientPhonetxt.Text + "' ").Rows[0][0].ToString();
                }
                DataTable ss = db.RunReader("select * from [Reserve] where Patient_id ='" + patientCodetxt.Text + "' ");
                if (ss.Rows.Count > 0)
                {
                    MessageBox.Show("تم الحجز من قبل برقم  " + ss.Rows[0][0].ToString());
                }
                else
                {
                    db.RunNonQuery("insert into [Reserve] (Patient_id) values ('" + patientCodetxt.Text + "') ");

                    ss = db.RunReader("select * from [Reserve] where Patient_id ='" + patientCodetxt.Text + "' ");
                    MessageBox.Show("تم الحجز رقم الحجز " + ss.Rows[0][0].ToString());
                }
                get_reserves();
            }

        }



        private void patientCodetxt_TextChanged(object sender, TextChangedEventArgs e)
        {
       
        }

        private void patientCodetxt_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                DataTable temp = db.RunReader("select * from [Patient] where [Id]='" + patientCodetxt.Text + "' ");
                if (temp.Rows.Count > 0)
                {
                    patientCodetxt.Text = temp.Rows[0][0].ToString();
                    patientIDtxt.Text = temp.Rows[0][1].ToString();
                    patientEnametxt.Text = temp.Rows[0][2].ToString();
                    patientAnametxt.Text = temp.Rows[0][3].ToString();
                    if (temp.Rows[0][4].ToString() == "male")
                        maleRB.IsChecked = true;
                    else if (temp.Rows[0][4].ToString() == "female")
                        femaleRB.IsChecked = true;
                    patientPhonetxt.Text = temp.Rows[0][5].ToString();
                    patientBDtxt.Text = temp.Rows[0][6].ToString();
                    patientCitytxt.Text = temp.Rows[0][7].ToString();
                    patientAreatxt.Text = temp.Rows[0][8].ToString();

                }
                else
                    MessageBox.Show("لا يوجد بيانات");
            }
        }

        private void editPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (patientIDtxt.Text.Length != 14)
            {
                MessageBox.Show("من فضلك ادخل 14 رقم للبطاقة");
            }
            else
            {
                String gender = "";
                if (maleRB.IsChecked == true)
                {
                    gender = "male";
                }

                else if (femaleRB.IsChecked == true)
                {
                    gender = "female";
                }
                string da = patientBDtxt.SelectedDate.Value.ToString("yyyy-MM-dd");

                db.RunNonQuery(@"update  [Patient] set [aname]=N'" + patientAnametxt.Text + "' , [ename]='" + patientEnametxt.Text + "' , [dateofbirth]='" + da + "' " +
                    ", [ssn]='" + patientIDtxt.Text + "' , [phone]='" + patientPhonetxt.Text + "' , [gender]='" + gender + "' , [city]=N'" + patientCitytxt.Text + "'" +
                    ", [area]=N'" + patientCitytxt.Text + "' where [id]='" + patientCodetxt.Text + "' ", "تم التعديل");

                get_reserves();
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            object item = reserveGrid.SelectedItem;
            DataGridRow drow = reserveGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
            string serial = (reserveGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text.ToString();
            MessageBoxResult result = MessageBox.Show("هل أنت متأكد", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                db.RunNonQuery("delete from  [reserve]  where [serial]='" + serial + "' ", "تم المسح بنجاح");
                get_reserves();
            }
            else
            { }
        }

        private void srchCodeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataTable temp = db.RunReader("select * from [Patient] where [Id]='" + patientCodetxt.Text + "' ");
            if (temp.Rows.Count > 0)
            {
                patientCodetxt.Text = temp.Rows[0][0].ToString();
                patientIDtxt.Text = temp.Rows[0][1].ToString();
                patientEnametxt.Text = temp.Rows[0][2].ToString();
                patientAnametxt.Text = temp.Rows[0][3].ToString();
                if (temp.Rows[0][4].ToString() == "male")
                    maleRB.IsChecked = true;
                else if (temp.Rows[0][4].ToString() == "female")
                    femaleRB.IsChecked = true;
                patientPhonetxt.Text = temp.Rows[0][5].ToString();
                patientBDtxt.Text = temp.Rows[0][6].ToString();
                patientCitytxt.Text = temp.Rows[0][7].ToString();
                patientAreatxt.Text = temp.Rows[0][8].ToString();

            }
            else
                MessageBox.Show("لا يوجد بيانات");
        }

        private void patientCitytxt_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                patientAreatxt.ItemsSource = db.RunReader("select * from [AREA] where [Governorate] ='" + gover.Rows[patientCitytxt.SelectedIndex][0] + "' order by [AREA_CODE]").DefaultView;
            }
            catch { }
        }

        private void patientPhonetxt_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable temp = db.RunReader("select * from [Patient] where [phone]='" + patientPhonetxt.Text + "' ");
            if (temp.Rows.Count > 0 && patientPhonetxt.Text != "")
            {
                patientCodetxt.Text = temp.Rows[0][0].ToString();
                patientIDtxt.Text = temp.Rows[0][1].ToString();
                patientEnametxt.Text = temp.Rows[0][2].ToString();
                patientAnametxt.Text = temp.Rows[0][3].ToString();
                if (temp.Rows[0][4].ToString() == "male")
                    maleRB.IsChecked = true;
                else if (temp.Rows[0][4].ToString() == "female")
                    femaleRB.IsChecked = true;
                patientPhonetxt.Text = temp.Rows[0][5].ToString();
                patientBDtxt.Text = temp.Rows[0][6].ToString();
                patientCitytxt.Text = temp.Rows[0][7].ToString();
                patientAreatxt.Text = temp.Rows[0][8].ToString();

            }
        }

        private void patientIDtxt_KeyUp(object sender, KeyEventArgs e)
        {
            DataTable temp = db.RunReader("select * from [Patient] where [ssn]='" + patientIDtxt.Text + "' ");
            if (temp.Rows.Count > 0 && patientIDtxt.Text != "")
            {
                patientCodetxt.Text = temp.Rows[0][0].ToString();
                patientIDtxt.Text = temp.Rows[0][1].ToString();
                patientEnametxt.Text = temp.Rows[0][2].ToString();
                patientAnametxt.Text = temp.Rows[0][3].ToString();
                if (temp.Rows[0][4].ToString() == "male")
                    maleRB.IsChecked = true;
                else if (temp.Rows[0][4].ToString() == "female")
                    femaleRB.IsChecked = true;
                patientPhonetxt.Text = temp.Rows[0][5].ToString();
                patientBDtxt.Text = temp.Rows[0][6].ToString();
                patientCitytxt.Text = temp.Rows[0][7].ToString();
                patientAreatxt.Text = temp.Rows[0][8].ToString();

            }
        }

        private void patientIDtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(patientIDtxt.Text, "[^0-9]"))
                {
                    patientIDtxt.Text = patientIDtxt.Text.Remove(patientIDtxt.Text.Length - 1);
                }

                if (patientIDtxt.Text.Length > 14)
                {
                    MessageBox.Show("من فضلك ادخل 14 رقم للبطاقة");
                }
                else { }
            }
            catch { }
        }

        private void patientPhonetxt_TextChanged_1(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(patientPhonetxt.Text, "[^0-9]"))
                {
                    patientPhonetxt.Text = patientPhonetxt.Text.Remove(patientPhonetxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Log_In s = new Log_In();
            s.Show();
        }
    }
}
