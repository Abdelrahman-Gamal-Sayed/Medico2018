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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Medico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class adminForm : Window
    {
        public adminForm()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            refdata();
            refdataray();
            refreshLabData();
            diagdataray();
            refreshDoctor();
            refaa();
        }
        void refdata()
        {
            DataTable a = db.RunReader("select * from Medicine");
            a.Columns[0].ColumnName= "كود الدواء";
            a.Columns[1].ColumnName = "اسم الدواء باللغة العربية";
            a.Columns[2].ColumnName = "اسم الدواء باللغة الانجليزية";
            a.Columns[3].ColumnName = "نوع الدواء";
            medicineGrid.ItemsSource = a.DefaultView;
        }
        void refdataray()
        {
            DataTable a = db.RunReader("select * from Ray");
            a.Columns[0].ColumnName = "كود الاشاعة";
            a.Columns[1].ColumnName = "اسم الاشاعة باللغة العربية";
            a.Columns[2].ColumnName = "اسم الاشاعة باللغة الانجليزية";
            rayGrid.ItemsSource = a.DefaultView;
        }

        void diagdataray()
        {
            DataTable a = db.RunReader("select * from Diagnosis");
            a.Columns[0].ColumnName = "كود التشخيص";
            a.Columns[1].ColumnName = "اسم التشخيص باللغة العربية";
            a.Columns[2].ColumnName = "اسم التشخيص باللغة الانجليزية";
            labGrid1.ItemsSource = a.DefaultView;
        }


        private void refreshDoctor()
        {
            DataSet empData = db.RunReaderds("select distinct type_code,type_name from usertype");
            employeeTypeCombo.ItemsSource = empData.Tables[0].DefaultView;

            DataTable a = db.RunReader(@"select u.id,u.name,u.password,ut.type_name
                                    from[USER] u
                                    join[UserType] ut
                                    on ut.type_code = u.type");
            a.Columns[0].ColumnName = "كود الموظف";
            a.Columns[1].ColumnName = "اسم المستخدم";
            a.Columns[2].ColumnName = "كلمة السر";
            a.Columns[3].ColumnName = "نوع الموظف";
            doctorGrid.ItemsSource = a.DefaultView;
        }
        private void refreshLabData()
        {
            DataTable a = db.RunReader("select * from LAB");
            a.Columns[0].ColumnName = "كود التحليل";
            a.Columns[1].ColumnName = "اسم التحليل باللغة العربية";
            a.Columns[2].ColumnName = "اسم التحليل باللغة الانجليزية";
            labGrid.ItemsSource = a.DefaultView;
        }
        DB db = new DB();
        private void deleteMedicineBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if(medicineGrid.SelectedItems.Count<=0)
                {
                    MessageBox.Show("من فضلك اختر الدواء الذي تريد مسحه");
                }
                else
                {
                  MessageBoxResult result=  MessageBox.Show("هل أنت متأكد", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.RunNonQuery("delete from  Medicine  where Medication_code='" + medicineCodetxt.Text + "' ", "تم المسح بنجاح");
                        medicineAnametxt.Text = "";
                        medicineEnametxt.Text = "";
                        medicineFormtxt.Text = "";
                        medicineCodetxt.Text = "";
                        refdata();
                    }else
                    { }
                }
            }
            catch { }
        }

        private void saveMeidicineBtn_Click(object sender, RoutedEventArgs e)
        {
            if (medicineEnametxt.Text == "")
            { MessageBox.Show("برجاء ادخل اسم الدواء"); }
            else
            {
                db.RunNonQuery(@"insert into Medicine (Medication_Aname,Medication_Ename,Medication_FORM) values  
                             (N'" + medicineAnametxt.Text + "','" + medicineEnametxt.Text + "','" + medicineFormtxt.Text + "')", "تم تسجيل دواء جديد");

                refdata();
                medicineAnametxt.Text = "";
                medicineEnametxt.Text = "";
                medicineFormtxt.Text = "";
                medicineCodetxt.Text = "";
            }

        }

        private void medicineGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)medicineGrid.SelectedItems[0];
                medicineAnametxt.Text = row[1].ToString();
                medicineEnametxt.Text = row[2].ToString();
                medicineFormtxt.Text = row[3].ToString();
                medicineCodetxt.Text = row[0].ToString();
            }
            catch { }
          
        }

        private void editMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("update  Medicine set Medication_Aname=N'" + medicineAnametxt.Text + "' , Medication_Ename='" + medicineEnametxt.Text + "' , Medication_FORM='" + medicineFormtxt.Text + "' where Medication_code='" + medicineCodetxt.Text + "' ","تم التعديل");
            refdata();
        }

        private void newMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            medicineAnametxt.Text = "";
            medicineEnametxt.Text = "";
            medicineFormtxt.Text = "";
            medicineCodetxt.Text = "";
            medicineSrchtxt.Text = "";
            refdata();
        }
        private void medicineSrchtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable a = db.RunReader("select * from Medicine where Medication_Aname like '%" + medicineSrchtxt.Text + "%' or Medication_Ename like '%" + medicineSrchtxt.Text + "%' or Medication_FORM like '%" + medicineSrchtxt.Text + "%' or Medication_code like '%" + medicineSrchtxt.Text + "%'");
            a.Columns[0].ColumnName = "كود الدواء";
            a.Columns[1].ColumnName = "اسم الدواء باللغة العربية";
            a.Columns[2].ColumnName = "اسم الدواء باللغة الانجليزية";
            a.Columns[3].ColumnName = "نوع الدواء";
            medicineGrid.ItemsSource = a.DefaultView;
        }

        private void MedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            MedicineGroupBox.Visibility = Visibility.Visible;
            RAYGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox.Visibility = Visibility.Hidden;
                LabGroupBox_Copy.Visibility=Visibility.Hidden;
            DoctorGroupBox.Visibility = Visibility.Hidden;
            DoctorGroupBox_Copy.Visibility = Visibility.Hidden;
        }

        private void saveRayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (rayEnametxt.Text != "")
            {
                db.RunNonQuery("insert into Ray ([Ray_aname],[Ray_ename]) values (N'" + rayAnametxt.Text + "','" + rayEnametxt.Text + "')", "تم الحفظ بنجاح");
                refdataray();
            }
            else
            {
                MessageBox.Show("برجاء ادخال كل البيانات");
            }
        }

        private void rayBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorGroupBox.Visibility = Visibility.Hidden;
            DoctorGroupBox_Copy.Visibility = Visibility.Hidden;

            MedicineGroupBox.Visibility = Visibility.Hidden;
            RAYGroupBox.Visibility = Visibility.Visible;
            LabGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox_Copy.Visibility = Visibility.Hidden;
        }

        private void newRayBtn_Click(object sender, RoutedEventArgs e)
        {
            rayCodetxt.Text = "";
            rayAnametxt.Text = "";
            rayEnametxt.Text = "";
            raySrchtxt.Text = "";
        }

        private void rayGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)rayGrid.SelectedItems[0];
                rayAnametxt.Text = row[1].ToString();
                rayEnametxt.Text = row[2].ToString();
                rayCodetxt.Text = row[0].ToString();
            }
            catch { }
        }

        private void editRayBtn_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("update  Ray set [Ray_Aname]=N'" + rayAnametxt.Text + "' , [Ray_Ename]='" + rayEnametxt.Text + "' where [Ray_code]='" + rayCodetxt.Text + "' ", "تم التعديل");
            refdataray();
        }

        private void deleteRayBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (rayGrid.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("من فضلك اختر الآشعة التي تريد مسحها");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("هل أنت متأكد", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.RunNonQuery("delete from  Ray  where Ray_code='" + rayCodetxt.Text + "' ", "تم المسح بنجاح");
                        raySrchtxt.Text = "";
                        rayEnametxt.Text = "";
                        rayAnametxt.Text = "";
                        rayCodetxt.Text = "";
                        refdataray();
                    }
                    else
                    { }
                }
            }
            catch { }
        }

        private void raySrchtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable a = db.RunReader("select * from Ray where Ray_code like '%" + raySrchtxt.Text + "%' or Ray_Ename like '%" + raySrchtxt.Text + "%' or Ray_Aname like '%" + raySrchtxt.Text + "%'");
            a.Columns[0].ColumnName = "كود الاشاعة";
            a.Columns[1].ColumnName = "اسم الاشاعة باللغة العربية";
            a.Columns[2].ColumnName = "اسم الاشاعة باللغة الانجليزية";
            rayGrid.ItemsSource = a.DefaultView;
        }

        private void labBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorGroupBox.Visibility = Visibility.Hidden;
            DoctorGroupBox_Copy.Visibility = Visibility.Hidden;

            MedicineGroupBox.Visibility = Visibility.Hidden;
            RAYGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox.Visibility = Visibility.Visible;
            LabGroupBox_Copy.Visibility = Visibility.Hidden;
        }

        private void saveLabBtn_Click(object sender, RoutedEventArgs e)
        {
            if (labEnametxt.Text != "")
            {
                db.RunNonQuery("insert into lab ([lab_aname],[lab_ename]) values (N'" + labAnametxt.Text + "','" + labEnametxt.Text + "')", "تم الحفظ بنجاح");
                refreshLabData();
            }
            else
                MessageBox.Show("برجاء ادخال كل البيانات");
        }

        private void labGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                DataRowView row = (DataRowView)labGrid.SelectedItems[0];
                labAnametxt.Text = row[1].ToString();
                labEnametxt.Text = row[2].ToString();
                labCodetxt.Text = row[0].ToString();
            }
            catch { }
        }

        private void editLabBtn_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("update  LAB set [lab_aname]=N'" + labAnametxt.Text + "' , [lab_Ename]='" + labEnametxt.Text + "' where [lab_code]='" + labCodetxt.Text + "' ", "تم التعديل");
            refreshLabData();

        }

        private void deleteLabBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (labGrid.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("من فضلك اختر التحليل الذي تريد مسحه");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("هل أنت متأكد", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.RunNonQuery("delete from  lab  where lab_code='" + labCodetxt.Text + "' ", "تم المسح بنجاح");
                        labSrchtxt.Text = "";
                        labEnametxt.Text = "";
                        labAnametxt.Text = "";
                        labCodetxt.Text = "";
                        refreshLabData();
                    }
                    else
                    { }
                }
            }
            catch { }
        }

        private void labSrchtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable a = db.RunReader("select * from lab where lab_code like '%" + labSrchtxt.Text + "%' or lab_ename like '%" + labSrchtxt.Text + "%' or lab_aname like '%" + labSrchtxt.Text + "%'");
            a.Columns[0].ColumnName = "كود التحليل";
            a.Columns[1].ColumnName = "اسم التحليل باللغة العربية";
            a.Columns[2].ColumnName = "اسم التحليل باللغة الانجليزية";
            labGrid.ItemsSource = a.DefaultView;
        }

        private void newLabBtn_Click(object sender, RoutedEventArgs e)
        {
            labCodetxt.Text = "";
            labAnametxt.Text = "";
            labEnametxt.Text = "";
            labSrchtxt.Text = "";
        }

    

        private void saveLabBtn1_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"insert into diagnosis (Diagnosis_ANAME,Diagnosis_ENAME) values  
                             (N'" + labAnametxt1.Text + "','" + labEnametxt1.Text + "')", "تم تسجيل ");
            labCodetxt1.Text = "";
            labAnametxt1.Text = "";
            labEnametxt1.Text = "";
            labSrchtxt1.Text = "";
            diagdataray();
        }

        private void newLabBtn1_Click(object sender, RoutedEventArgs e)
        {
            labCodetxt1.Text = "";
            labAnametxt1.Text = "";
            labEnametxt1.Text = "";
            labSrchtxt1.Text = "";
            diagdataray();
        }

        private void labGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)labGrid1.SelectedItems[0];
                labAnametxt1.Text = row[1].ToString();
                labEnametxt1.Text = row[2].ToString();
                labCodetxt1.Text = row[0].ToString();
            }
            catch { }
        }

        private void editLabBtn1_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("update  diagnosis set [Diagnosis_ANAME]=N'" + labAnametxt1.Text + "' , [Diagnosis_ENAME]='" + labEnametxt1.Text + "' where [Diagnosis_Code]='" + labCodetxt1.Text + "' ", "تم التعديل");
            diagdataray();
        }

        private void labSrchtxt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable a = db.RunReader("select * from diagnosis where Diagnosis_ANAME like '%" + labSrchtxt1.Text + "%' or Diagnosis_ENAME like '%" + labSrchtxt1.Text + "%' or Diagnosis_Code like '%" + labSrchtxt1.Text + "%'");
            a.Columns[0].ColumnName = "كود التشخيص";
            a.Columns[1].ColumnName = "اسم التشخيص باللغة العربية";
            a.Columns[2].ColumnName = "اسم التشخيص باللغة الانجليزية";
            labGrid1.ItemsSource = a.DefaultView;
        }

        private void deleteLabBtn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (labGrid1.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("من فضلك اختر التشخيص الذي تريد مسحه");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("هل أنت متأكد", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.RunNonQuery("delete from  diagnosis  where Diagnosis_Code='" + labCodetxt1.Text + "' ", "تم المسح بنجاح");
                        labCodetxt1.Text = "";
                        labAnametxt1.Text = "";
                        labEnametxt1.Text = "";
                        labSrchtxt1.Text = "";
                        diagdataray();
                    }
                    else
                    { }
                }
            }
            catch { }
        }

        private void diag_Click(object sender, RoutedEventArgs e)
        {
            MedicineGroupBox.Visibility = Visibility.Hidden;
            RAYGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox_Copy.Visibility = Visibility.Visible;
            DoctorGroupBox.Visibility = Visibility.Hidden;
            DoctorGroupBox_Copy.Visibility = Visibility.Hidden;

        }

        private void doctorBtn_Click(object sender, RoutedEventArgs e)
        {
            MedicineGroupBox.Visibility = Visibility.Hidden;
            RAYGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox_Copy.Visibility = Visibility.Hidden;
            DoctorGroupBox.Visibility = Visibility.Visible;
            DoctorGroupBox_Copy.Visibility = Visibility.Hidden;

        }

        private void saveDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"insert into [USER] (name,password,type) values  
                             (N'" + doctorNametxt.Text + "','" + doctorPasstxt.Text + "', '"+employeeTypeCombo.Text+"')", "تم تسجيل مستخدم جديد ");
            doctorCodetxt.Text = "";
            doctorNametxt.Text = "";
            doctorPasstxt.Text = "";
            doctorSrchtxt.Text = "";
            employeeTypeCombo.Text = "";
            refreshDoctor();
        }

        private void editDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("update  [USER] set [name]=N'" + doctorNametxt.Text + "' , [password]='" + doctorPasstxt.Text + "', [type]='" + employeeTypeCombo.Text + "' where [id]='" + doctorCodetxt.Text + "' ", "تم التعديل");
            refreshDoctor();
        }

        private void doctorGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)doctorGrid.SelectedItems[0];
                doctorNametxt.Text = row[1].ToString();
                doctorPasstxt.Text = row[2].ToString();
                doctorCodetxt.Text = row[0].ToString();
                employeeTypeCombo.Text = db.RunReader("select  type_code from usertype where type_name ='" + row[3].ToString() + "'").Rows[0][0].ToString();
            }
            catch { }
        }

        private void deleteDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (doctorGrid.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("من فضلك اختر المستخدم الذي تريد مسحه");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("هل أنت متأكد", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.RunNonQuery("delete from  [USER]  where id='" + doctorCodetxt.Text + "' ", "تم المسح بنجاح");
                        doctorCodetxt.Text = "";
                        doctorNametxt.Text = "";
                        doctorPasstxt.Text = "";
                        doctorSrchtxt.Text = "";
                        employeeTypeCombo.Text = "";
                        refreshDoctor();
                    }
                    else
                    { }
                }
            }
            catch { }
        }

        private void newDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            doctorCodetxt.Text = "";
            doctorNametxt.Text = "";
            doctorPasstxt.Text = "";
            doctorSrchtxt.Text = "";
            employeeTypeCombo.Text = "";
            refreshDoctor();

        }

        private void doctorSrchtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable a = db.RunReader(@"select u.id,u.name,u.password,ut.type_name
                                    from [USER] u join  [UserType] ut on ut.type_code=u.type
                        where ut.type_name like '%"+doctorSrchtxt.Text+"%' or u.name like '%"+doctorSrchtxt.Text+"%'  or u.Id like '%"+doctorSrchtxt.Text+"%' ");
            a.Columns[0].ColumnName = "كود المستخدم";
            a.Columns[1].ColumnName = "اسم المستخدم";
            a.Columns[2].ColumnName = "كلمة السر";
            a.Columns[3].ColumnName = "نوع المستخدم";
            doctorGrid.ItemsSource = a.DefaultView;
        }

        private void saveDoctorBtn1_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"insert into [UserType] (type_name) values  
                             (N'" + doctorNametxt1.Text + "')", "تم تسجيل ");
            doctorNametxt1.Text = "";
            refaa();


        }
       void refaa()
        {
            doctorGrid1.ItemsSource = db.RunReader("select * from UserType").DefaultView;
        }

        private void doctorGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)doctorGrid1.SelectedItems[0];
                doctorNametxt1.Text = row[1].ToString();
                
                doctorCodetxt1.Text = row[0].ToString();

            }
            catch { }
        }

        private void editDoctorBtn1_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("update  [UserType] set [type_name]=N'" + doctorNametxt1.Text + "'  where [type_code]='" + doctorCodetxt1.Text + "' ", "تم التعديل");
            refaa();
        }

        private void deleteDoctorBtn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (doctorGrid1.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("من فضلك اختر المستخدم الذي تريد مسحه");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("هل أنت متأكد", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.RunNonQuery("delete from  [UserType]  where [type_code]='" + doctorCodetxt1.Text + "' ", "تم المسح بنجاح");
                        doctorCodetxt1.Text = "";
                        doctorNametxt1.Text = "";
                        refaa();
                    }
                    else
                    { }
                }
            }
            catch { }
        }

        private void diag_Copy_Click(object sender, RoutedEventArgs e)
        {
            MedicineGroupBox.Visibility = Visibility.Hidden;
            RAYGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox.Visibility = Visibility.Hidden;
            LabGroupBox_Copy.Visibility = Visibility.Hidden;
            DoctorGroupBox.Visibility = Visibility.Hidden;
            DoctorGroupBox_Copy.Visibility = Visibility.Visible;
        }

        private void editDoctorBtn1_Copy_Click(object sender, RoutedEventArgs e)
        {
            doctorCodetxt1.Text = "";
            doctorNametxt1.Text = "";
            refaa();
        }

        private void imgStack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(mainStack.Visibility==Visibility.Visible)
            {
                mainStack.Visibility = Visibility.Hidden;
            }
            else if(mainStack.Visibility==Visibility.Hidden)
            {
                mainStack.Visibility = Visibility.Visible;
            }
        }

        private void diag_Copy1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Log_In s = new Log_In();
            s.Show();
        }
    }
}
