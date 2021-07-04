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
namespace Medico
{
    /// <summary>
    /// Interaction logic for details.xaml
    /// </summary>
    public partial class details : Window
    {
        public details(string patient)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DataTable data = db.RunReader("select * from detection where patient_id =" + patient +"order by detection_date");
            data.Columns[0].ColumnName = "رقم الروشتة";
            data.Columns[1].ColumnName = "رقم المريض";
            data.Columns[2].ColumnName = "تاريخ الكشف";
            data.Columns[3].ColumnName = "ملاحظات";
            detectionGrid.ItemsSource = data.DefaultView;

        }
        DB db = new DB();

        private void detectionGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)detectionGrid.SelectedItems[0];
                string detectionID = row[0].ToString();



                finalDatetxt.Text = "";
                finaldiagnosetxt.Text = "";
                finalMedtxt.Text = "";
                finalraytxt.Text = "";
                finalLabtxt.Text = "";
                finalNotetxt.Text = "";

              
                    finalDatetxt.Text =(Convert.ToDateTime( row[2].ToString())).ToShortDateString();
                finalNotetxt.Text = row[3].ToString();
                DataTable diagnoseDT = db.RunReader("select diagnosis.diagnosis_ename from diagnosis , DiagnoseDetection  where DiagnoseDetection.det_id= " + detectionID + " and diagnosis.Diagnosis_Code= DiagnoseDetection.diagnose_id ");
                    for (int i = 0; i < diagnoseDT.Rows.Count; i++)
                    {
                        finaldiagnosetxt.Text = finaldiagnosetxt.Text + diagnoseDT.Rows[i][0].ToString() + "\n";
                    }

                    DataTable labDT = db.RunReader("select lab.lab_ename from lab , LabDetection where LabDetection.det_id=" + detectionID + " and lab.lab_code=LabDetection.lab_code");
                    for (int i = 0; i < labDT.Rows.Count; i++)
                    {
                        finalLabtxt.Text = finalLabtxt.Text + labDT.Rows[i][0].ToString() + "\n";
                    }

                    DataTable rayDT = db.RunReader("select ray.ray_ename from ray , rayDetection where rayDetection.det_id=" + detectionID + " and ray.ray_code=rayDetection.ray_code");
                    for (int i = 0; i < labDT.Rows.Count; i++)
                    {
                        finalraytxt.Text = finalraytxt.Text + rayDT.Rows[i][0].ToString() + "\n";
                    }

                    DataTable medDT = db.RunReader("select  Medicine.Medication_Ename, MedicineDetection.med_dose, MedicineDetection.med_duration  from Medicine, MedicineDetection where  MedicineDetection.det_id = " + detectionID + " and Medicine.Medication_code = MedicineDetection.Med_code");
                    for (int i = 0; i < medDT.Rows.Count; i++)
                    {
                        finalMedtxt.Text = finalMedtxt.Text + medDT.Rows[i][0].ToString() + " , " + medDT.Rows[i][1].ToString() + " , " + medDT.Rows[i][2].ToString() + "\n";
                    }



                


            }
            catch { }
        }
    }
}
