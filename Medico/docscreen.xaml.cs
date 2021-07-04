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
    /// Interaction logic for docscreen.xaml
    /// </summary>

        struct dawa
    {
      public  string name, dose, dur;
    };

    public partial class docscreen : Window
    {
        public docscreen()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //-----------------------------------------------------------------------------------------------------------------------
            patient = db.RunReader("select * from [Reserve] order by [serial] ");
            if (patient.Rows.Count <= 0)
            {
                MessageBox.Show("لا يوجد مرضى فى الكشف");
            }
            else
            {
                reserveCodetxt.Text = patient.Rows[0][0].ToString();
                patientCodetxt.Text = patient.Rows[0][1].ToString();
                patient = db.RunReader("select * from [Patient]  where [Id] = '" + patient.Rows[0][1].ToString() + "'");
                patientAnametxt.Text = patient.Rows[0][3].ToString();
                patientEnametxt.Text = patient.Rows[0][2].ToString();
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(Convert.ToDateTime(patient.Rows[0][6].ToString()).ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                agetxt.Text = age.ToString();
            }

            //----------------------------------------------------------------------------------------------------------------------




            lastdetection = db.RunReader("select * from [detection] where patient_id='"+patientCodetxt.Text+"' order by detection_date ");
            if (lastdetection.Rows.Count <= 0)
            {
                //MessageBox.Show("لا يوجد مرضى فى الكشف");
                finalDatetxt.Text = "لا يوجد كشف سابق";
                finaldiagnosetxt.Text = "لا يوجد كشف سابق";
                finalLabtxt.Text = "لا يوجد كشف سابق";
                finalraytxt.Text = "لا يوجد كشف سابق";
                finalMedtxt.Text = "لا يوجد كشف سابق";
                finalNotetxt.Text = "لا يوجد كشف سابق";
            }
            else
            {
                string detectionID = lastdetection.Rows[0][0].ToString();

                finalDatetxt.Text = (Convert.ToDateTime(lastdetection.Rows[0][2].ToString())).ToShortDateString();
                finalNotetxt.Text = lastdetection.Rows[0][3].ToString();
                DataTable diagnoseDT = db.RunReader("select diagnosis.diagnosis_ename from diagnosis , DiagnoseDetection  where DiagnoseDetection.det_id= "+ detectionID + " and diagnosis.Diagnosis_Code= DiagnoseDetection.diagnose_id ");
                for (int i = 0; i < diagnoseDT.Rows.Count; i++)
                {
                    finaldiagnosetxt.Text = finaldiagnosetxt.Text + diagnoseDT.Rows[i][0].ToString()+"\n";
                }

                DataTable labDT = db.RunReader("select lab.lab_ename from lab , LabDetection where LabDetection.det_id="+detectionID+" and lab.lab_code=LabDetection.lab_code");
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


            //----------------------------------------------------------------------------------------------------------------------


            load_data();
        }
        DB db = new DB();
        DataTable patient ,lastdetection;
        int index = 0;
        List<string> diagnoseList = new List<string>();
        List<dawa> medicinefullList = new List<dawa>();
        List<string> medicineList = new List<string>();
        List<string> rayList = new List<string>();
        List<string> labList = new List<string>();
        private void load_data()
        {
            DataTable diagnoseDT = db.RunReader("select * from diagnosis order by diagnosis_code");
            diagnoseCombo.ItemsSource = diagnoseDT.DefaultView;

            DataTable medicineDT = db.RunReader("select * from medicine order by Medication_code");
            medicineCombo.ItemsSource = medicineDT.DefaultView;

            DataTable rayDT = db.RunReader("select * from ray order by Ray_code");
            rayCombo.ItemsSource = rayDT.DefaultView;

            DataTable labDT = db.RunReader("select * from lab order by lab_code");
            labCombo.ItemsSource = labDT.DefaultView;
        }

     
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           

            }

        private void diagnoseCombo_KeyUp(object sender, KeyEventArgs e)
        {
            diagnoseCombo.ItemsSource = db.RunReader("select * from diagnosis where Diagnosis_Code like '%" + diagnoseCombo.Text + "%' or Diagnosis_ANAME like N'%" + diagnoseCombo.Text + "%' order by diagnosis_code").DefaultView;
            diagnoseCombo.IsDropDownOpen = true;
        }

        private void medicineCombo_KeyUp(object sender, KeyEventArgs e)
        {
            //medicineCombo.ItemsSource = db.RunReader("select * from medicine where medication_code like '%" + medicineCombo.Text + "%' or medication_ename like '%" + medicineCombo.Text + "%' order by medication_code").DefaultView;
            //medicineCombo.IsDropDownOpen = true;
        }

        private void rayCombo_KeyUp(object sender, KeyEventArgs e)
        {
            rayCombo.ItemsSource = db.RunReader("select * from ray where ray_code like '%" + rayCombo.Text + "%' or ray_ename like '%" + rayCombo.Text + "%' order by ray_code").DefaultView;
            rayCombo.IsDropDownOpen = true;
        }

        private void labCombo_KeyUp(object sender, KeyEventArgs e)
        {
            labCombo.ItemsSource = db.RunReader("select * from lab where lab_code like '%" + labCombo.Text + "%' or lab_ename like '%" + labCombo.Text + "%' order by lab_code").DefaultView;
            labCombo.IsDropDownOpen = true;
        }

        private void notetxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            newnotetxt.Text = notetxt.Text;
        }
        bool flag = false;
        private void addDiagnoseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!diagnoseList.Contains(diagnoseCombo.Text))
            {
                 DataTable tt = db.RunReader("select diagnosis_aname from diagnosis where diagnosis_code ='" + diagnoseCombo.Text + "' ");
                string name;
                if (tt.Rows.Count <= 0)
                {

                    MessageBox.Show("هذا التشخيص غير مسجل");
                }
                else
                {
                    diagnoseList.Add(diagnoseCombo.Text);

                    name = tt.Rows[0][0].ToString();

                    if (flag == false )
                    {
                        diagnosetxt.Text = name;
                        flag = true;
                    }
                    else
                    {
                        diagnosetxt.Text = diagnosetxt.Text + " , " + name;
                    }
                    diagnoseCombo.Text = "";
              

                }

                DataTable diagnoseDT = db.RunReader("select * from diagnosis order by diagnosis_code");
                diagnoseCombo.ItemsSource = diagnoseDT.DefaultView;


            }
            else
                MessageBox.Show("تم اضافة هذا التشخيص من قبل");

        }

        private void deleteDiagnoseBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (diagnoseList.Contains(diagnoseCombo.Text))
            {
                diagnoseList.Remove(diagnoseCombo.Text);
                diagnoseCombo.Text = "";
                int x = 0;
                if (diagnoseList.Count <= 0)
                {
                    diagnosetxt.Text = "";
                    flag = false;
                }
                else
                {
                  
                   
                        string name;

                    foreach (string item in diagnoseList)
                        {

                        DataTable sss = db.RunReader("select diagnosis_aname from diagnosis where diagnosis_code ='" + item + "' ");

                        if (sss.Rows.Count <= 0)

                        {
                                name = item;

                        }

                        else

                        {
                                name = sss.Rows[0][0].ToString();

                        }




                        if (x == 0)
                        {
                            diagnosetxt.Text = name;
                            x++;
                        }
                        else
                            diagnosetxt.Text = diagnosetxt.Text + " , " + name;

                   
                    }
                }
            }
            else
            { MessageBox.Show("غير موجود فى روشتة هذا المريض"); }
        }

        private void addMedicineBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!medicineList.Contains(medicineCombo.Text))
            {
              DataTable tt = db.RunReader("select medication_ename from medicine where Medication_code ='" + medicineCombo.Text + "' ");
                string name;

                dawa nn;

                if (tt.Rows.Count <= 0)
                {

                    MessageBox.Show("هذا الدواء غير مسجل");
                }
                else
                {
                    medicineList.Add(medicineCombo.Text);


                    name = tt.Rows[0][0].ToString();
                    nn.name = name;
                    nn.dose = patientAnametxt_Copy.Text;
                    nn.dur = patientAnametxt_Copy1.Text;
                    medicinefullList.Add(nn);
                    medicinetxt.Text = medicinetxt.Text + name + " , " + patientAnametxt_Copy.Text + " , " + patientAnametxt_Copy1.Text + "\n";
                    medicineCombo.Text = "";
                    patientAnametxt_Copy.Text = "";
                    patientAnametxt_Copy1.Text = "";
                }


                DataTable medicineDT = db.RunReader("select * from medicine order by Medication_code");
                medicineCombo.ItemsSource = medicineDT.DefaultView;


            }
            else
                MessageBox.Show("تم اضافة هذا الدواء من قبل");
        }

        private void deleteMedicineBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (medicineList.Contains(medicineCombo.Text))
            {
              int ind=  medicineList.IndexOf(medicineCombo.Text);
                medicineList.Remove(medicineCombo.Text);
                medicinefullList.RemoveAt(ind);
                medicineCombo.Text = "";
                int x = 0;
                if (medicineList.Count <= 0)
                {
                    medicinetxt.Text = "";
                }
                else
                {
                    medicinetxt.Text = "";

                    foreach (dawa item in medicinefullList)
                        {


                        medicinetxt.Text = medicinetxt.Text + item.name + " , " + item.dose + " , " + item.dur + "\n";

                    }
                }
            }
            else
            { MessageBox.Show("غير موجود فى روشتة هذا المريض"); }
        }
        bool flag2 = false;
        private void addRayBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!rayList.Contains(rayCombo.Text))
            {
              
                DataTable tt = db.RunReader("select ray_ename from ray where ray_code ='" + rayCombo.Text + "' ");
                string name;
                if (tt.Rows.Count <= 0)
                {

                    MessageBox.Show("هذه الاشاعة غير مسجلة");
                }
                else
                {
                    rayList.Add(rayCombo.Text);
                    name = tt.Rows[0][0].ToString();

                    if (flag2 == false )
                    {
                        raytxt.Text = name;
                        flag2 = true;
                    }
                    else
                    {
                        raytxt.Text = raytxt.Text + " , " + name;
                    }
                    rayCombo.Text = "";
             
                }

                DataTable rayDT = db.RunReader("select * from ray order by Ray_code");
                rayCombo.ItemsSource = rayDT.DefaultView;


            }
            else
                MessageBox.Show("تم اضافة هذه الاشعة من قبل");
        }

        private void deleteRayBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rayList.Contains(rayCombo.Text))
            {
                rayList.Remove(rayCombo.Text);
                rayCombo.Text = "";
                int x = 0;
                if (rayList.Count <= 0)
                {
                    raytxt.Text = "";
                    flag2 = false;
                }
                else
                {
                    string name;
                    foreach (string item in rayList)
                    {
                        DataTable sss = db.RunReader("select ray_ename from ray where ray_code ='" + item + "' ");
                        if (sss.Rows.Count <= 0)
                        {
                            name = item;
                        }
                        else
                        {
                             name = sss.Rows[0][0].ToString();
                        }

                        if (x == 0)
                        {
                            raytxt.Text = name;
                            x++;
                        }
                        else
                            raytxt.Text = raytxt.Text + " , " + name;

                    }
                }

            }
            else
            { MessageBox.Show("غير موجود فى روشتة هذا المريض"); }
        }
        bool flag3 = false;
        private void addLabBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!labList.Contains(labCombo.Text))
            {
               
                DataTable tt = db.RunReader("select lab_ename from lab where lab_code ='" + labCombo.Text + "' ");
                string name;
                if (tt.Rows.Count <= 0)
                {

                    MessageBox.Show("هذا التحليل غير مسجل");
                }
                else
                {
                    labList.Add(labCombo.Text);
                    name = tt.Rows[0][0].ToString();

                    if (flag3 == false )
                    {
                        labtxt.Text = name;
                        flag3 = true;
                    }
                    else
                    {
                        labtxt.Text = labtxt.Text + " , " + name;
                    }
                    labCombo.Text = "";

             
                }
                DataTable labDT = db.RunReader("select * from lab order by lab_code");
                labCombo.ItemsSource = labDT.DefaultView;



            }
            else
                MessageBox.Show("تم اضافة هذا التحليل من قبل");
        }

        private void deleteLabBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (labList.Contains(labCombo.Text))
            {
                labList.Remove(labCombo.Text);
                labCombo.Text = "";
                int x = 0;
                if (labList.Count <= 0)
                {
                    labtxt.Text = "";
                    flag3 = false;
                }
                else
                {
                    string name;

                    foreach (string item in labList)
                    {

                        DataTable sss = db.RunReader("select lab_ename from lab where lab_code ='" + item + "' ");

                        if (sss.Rows.Count <= 0)

                        {
                            name = item;

                        }

                        else

                        {
                            name = sss.Rows[0][0].ToString();

                        }


                   
                        if (x == 0)
                        {
                            labtxt.Text = name;
                            x++;
                        }
                        else
                            labtxt.Text = labtxt.Text + " , " + name;

                    }
                }
            }
            else
            { MessageBox.Show("غير موجود فى روشتة هذا المريض"); }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string note = finalNotetxt.Text.ToString();
          db.RunNonQuery("update detection set detection_notes = N'" + note + "', new_date = CURRENT_TIMESTAMP where detection_id = (select max (detection_id) from detection where  patient_id = "+ patientCodetxt.Text+")", "تم تعديل الروشتة");
           
        }

        private void detailsBtn_Click(object sender, RoutedEventArgs e)
        {
            string patientid = patientCodetxt.Text.ToString();
            details det = new details(patientid);
            det.ShowDialog();

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            patient = db.RunReader("select * from [Reserve] order by [serial] ");
            if (patient.Rows.Count <= 0)
            {
                MessageBox.Show("لا يوجد مرضى فى الكشف");
            }
            else
            {
                index--;
                if (index < 0)
                {
                    MessageBox.Show(" هذا اول مريض فى الكشف");
                    index++;
                }
                else
                {

                    reserveCodetxt.Text = patient.Rows[index][0].ToString();
                    patientCodetxt.Text = patient.Rows[index][1].ToString();
                    patient = db.RunReader("select * from [Patient]  where [Id] = '" + patient.Rows[index][1].ToString() + "'");
                    patientAnametxt.Text = patient.Rows[0][3].ToString();
                    patientEnametxt.Text = patient.Rows[0][2].ToString();
                    int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                    int dob = int.Parse(Convert.ToDateTime(patient.Rows[0][6].ToString()).ToString("yyyyMMdd"));
                    int age = (now - dob) / 10000;
                    agetxt.Text = age.ToString();

                }


                //-----------------------------------------------------------------------------------------


                finalDatetxt.Text = "";
                finaldiagnosetxt.Text = "";
                finalMedtxt.Text = "";
                finalraytxt.Text = "";
                finalLabtxt.Text = "";
                finalNotetxt.Text = "";

                lastdetection = db.RunReader("select * from [detection] where patient_id='" + patientCodetxt.Text + "' order by detection_date ");
                if (lastdetection.Rows.Count <= 0)
                {
                    //MessageBox.Show("لا يوجد مرضى فى الكشف");
                    finalDatetxt.Text = "لا يوجد كشف سابق";
                    finaldiagnosetxt.Text = "لا يوجد كشف سابق";
                    finalLabtxt.Text = "لا يوجد كشف سابق";
                    finalraytxt.Text = "لا يوجد كشف سابق";
                    finalMedtxt.Text = "لا يوجد كشف سابق";
                    finalNotetxt.Text = "لا يوجد كشف سابق";
                }
                else
                {
                    string detectionID = lastdetection.Rows[0][0].ToString();
                    finalDatetxt.Text = (Convert.ToDateTime(lastdetection.Rows[0][2].ToString())).ToShortDateString();
                    finalNotetxt.Text = lastdetection.Rows[0][3].ToString();
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
            }
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            index++;
            patient = db.RunReader("select * from [Reserve] order by [serial] ");
            if (patient.Rows.Count <= index)
            {
                MessageBox.Show("لا يوجد مرضى اخرين فى الكشف");
                index--;
            }
            else
            {
                reserveCodetxt.Text = patient.Rows[index][0].ToString();
                patientCodetxt.Text = patient.Rows[index][1].ToString();
                patient = db.RunReader("select * from [Patient]  where [Id] = '" + patient.Rows[index][1].ToString() + "'");
                patientAnametxt.Text = patient.Rows[0][3].ToString();
                patientEnametxt.Text = patient.Rows[0][2].ToString();
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(Convert.ToDateTime(patient.Rows[0][6].ToString()).ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                agetxt.Text = age.ToString();

                //-----------------------------------------------------------------------------------------


                finalDatetxt.Text = "";
                finaldiagnosetxt.Text = "";
                finalMedtxt.Text = "";
                finalraytxt.Text = "";
                finalLabtxt.Text = "";
                finalNotetxt.Text = "";

                lastdetection = db.RunReader("select * from [detection] where patient_id='" + patientCodetxt.Text + "' order by detection_date ");
                if (lastdetection.Rows.Count <= 0)
                {
                    //MessageBox.Show("لا يوجد مرضى فى الكشف");
                    finalDatetxt.Text = "لا يوجد كشف سابق";
                    finaldiagnosetxt.Text = "لا يوجد كشف سابق";
                    finalLabtxt.Text = "لا يوجد كشف سابق";
                    finalraytxt.Text = "لا يوجد كشف سابق";
                    finalMedtxt.Text = "لا يوجد كشف سابق";
                    finalNotetxt.Text = "لا يوجد كشف سابق";
                }
                else
                {
                    string detectionID = lastdetection.Rows[0][0].ToString();
                    finalDatetxt.Text = (Convert.ToDateTime(lastdetection.Rows[0][2].ToString())).ToShortDateString();

                    finalNotetxt.Text = lastdetection.Rows[0][3].ToString();
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




            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Log_In s = new Log_In();
            s.Show();
        }

        private void medicineCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                medicineCombo.ItemsSource = db.RunReader("select * from medicine where medication_code like '%" + medicineCombo.Text + "%' or medication_ename like '%" + medicineCombo.Text + "%' order by medication_code").DefaultView;
                medicineCombo.IsDropDownOpen = true;
            }
        }

      

       

        private void aasd(object sender, TextChangedEventArgs e)
        {

            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(patientAnametxt_Copy.Text, "[^0-9]"))
                {
                    patientAnametxt_Copy.Text = patientAnametxt_Copy.Text.Remove(patientAnametxt_Copy.Text.Length - 1);
                }
            }
            catch { }
        }

        private void qweqwe(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(patientAnametxt_Copy1.Text, "[^0-9]"))
                {
                    patientAnametxt_Copy1.Text = patientAnametxt_Copy1.Text.Remove(patientAnametxt_Copy1.Text.Length - 1);
                }
            }
            catch { }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("insert into Detection (detection_date ,patient_id ,detection_notes) values (CURRENT_TIMESTAMP,'" + patientCodetxt.Text + "','" + newnotetxt.Text + "')");
            //===========================================================

            string id = db.RunReader("select max(Detection_id) from Detection ").Rows[0][0].ToString();
            //===========================================================

            foreach (string item in diagnoseList)
            {  db.RunNonQuery("insert into DiagnoseDetection (diagnose_id ,det_id ) values ('" +item  + "','" + id + "')");}
            diagnoseList.Clear();

            foreach (string item in rayList)
            { db.RunNonQuery("insert into RayDetection (ray_code ,det_id ) values ('" + item + "','" + id + "')"); }
            rayList.Clear();

            foreach (string item in labList)
            { db.RunNonQuery("insert into LabDetection (lab_code ,det_id ) values ('" + item + "','" + id + "')"); }
            labList.Clear();
            int ee = 0;
            foreach (dawa item in medicinefullList)
            {
                string code = medicineList[ee];
                db.RunNonQuery("insert into MedicineDetection (Med_code ,det_id,med_dose,med_duration ) values ('" + code + "','" + id + "','" + item.dose + "','" + item.dur + "')");
                ee++;
            }
            medicinefullList.Clear();
            medicineList.Clear();
            ee = 0;
            //===========================================================

            db.RunNonQuery("delete from reserve where serial ='" + reserveCodetxt.Text + "'");



            patient = db.RunReader("select * from [Reserve] order by [serial] ");
            if (patient.Rows.Count <= 0)
            {
                MessageBox.Show("لا يوجد مرضى فى الكشف");

                reserveCodetxt.Text = "";
                patientCodetxt.Text = "";
               patientAnametxt.Text = "";
                patientEnametxt.Text = "";
                agetxt.Text = "";


            }
            else
            {
                reserveCodetxt.Text = patient.Rows[0][0].ToString();
                patientCodetxt.Text = patient.Rows[0][1].ToString();
                patient = db.RunReader("select * from [Patient]  where [Id] = '" + patient.Rows[0][1].ToString() + "'");
                patientAnametxt.Text = patient.Rows[0][3].ToString();
                patientEnametxt.Text = patient.Rows[0][2].ToString();
                int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                int dob = int.Parse(Convert.ToDateTime(patient.Rows[0][6].ToString()).ToString("yyyyMMdd"));
                int age = (now - dob) / 10000;
                agetxt.Text = age.ToString();
            }

            //===========================================================

            MessageBox.Show(" تم تاكيد الكشف رقم الروشتة " + id);

            notetxt.Text = "";
            newnotetxt.Text = "";
            diagnosetxt.Text = "";
            medicinetxt.Text = "";
            labtxt.Text = "";
            raytxt.Text = "";

            //===========================================================

            finalDatetxt.Text = "";
            finaldiagnosetxt.Text = "";
            finalMedtxt.Text = "";
            finalraytxt.Text = "";
            finalLabtxt.Text = "";
            finalNotetxt.Text = "";

        }
    }
}
