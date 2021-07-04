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
    /// Interaction logic for labs.xaml
    /// </summary>
    public partial class labs : Window
    {
        public labs()
        {
            InitializeComponent();
        }


        DB db = new DB();
        private void patientCodetxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                fillro4ta();
            }
        }

        private void srchCodeBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fillro4ta();
        }
        void fillro4ta()
        {
            DataTable data = db.RunReader("select Detection_id,patient_id from detection where Lab_Med_Checked='N' AND patient_id =" + patientCodetxt.Text + " order by detection_date");
            if (data.Rows.Count <= 0)
            {
                MessageBox.Show("برجاء التحقق من كود المريض");
            }
            else
            {
                data.Columns[0].ColumnName = "رقم الروشتة";
                data.Columns[1].ColumnName = "رقم المريض";
                detectionGrid.ItemsSource = data.DefaultView;
            }
        }

        void fillro4ta2()
        {
            DataTable data = db.RunReader("select Detection_id,patient_id from detection where Lab_Med_Checked='N' AND patient_id =" + patientCodetxt.Text + " order by detection_date");
            if (data.Rows.Count <= 0)
            {
                detectionGrid.ItemsSource = null;
            }
            else
            {
                data.Columns[0].ColumnName = "رقم الروشتة";
                data.Columns[1].ColumnName = "رقم المريض";
                detectionGrid.ItemsSource = data.DefaultView;
            }
        }


        private void detectionGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fill();

        }
        void fill()
        {
            try
            {
                DataRowView row = (DataRowView)detectionGrid.SelectedItems[0];
                string detectionID = row[0].ToString();
                DataTable dt = db.RunReader(@"
select	
		Lab.lab_code,
		lab_ename
		 
from Lab
inner join LabDetection
on  LabDetection.lab_code = Lab.lab_code
inner join Detection
on det_id = Detection_id
inner join Patient
on Id = patient_id
where LabDetection.lab_checked='N' and Detection.Detection_id = '" + detectionID + "' ");

                if (dt.Rows.Count <= 0)
                {
                    reserveGrid.ItemsSource = null;
                    db.RunNonQuery("update [Detection] set Lab_Med_Checked='Y' where Detection_id = '" + detectionID + "'");
                    fillro4ta2();
                }
                else
                {
                    reserveGrid.ItemsSource = dt.DefaultView;
                }

            }
            catch { }
        }
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            object item = reserveGrid.SelectedItem;
            DataGridRow drow = reserveGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
            string serial = (reserveGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text.ToString();

            db.RunNonQuery("update   [LabDetection] set  [lab_checked]='Y'  where    [lab_code]='" + serial + "' ", "تم  بنجاح");
            fill();




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            patientCodetxt.Text = "";
            detectionGrid.ItemsSource = null;
            reserveGrid.ItemsSource = null;
        }

        private void Button_Click33(object sender, RoutedEventArgs e)
        {
            this.Close();
            Log_In s = new Log_In();
            s.Show();
        }
    }
}
