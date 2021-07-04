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
where LabDetection.lab_checked='N' and Detection.Detection_id =1