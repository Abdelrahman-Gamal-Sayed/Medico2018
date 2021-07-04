
select	Ray.Ray_code,
		Ray_Ename
		 
from Ray
inner join RayDetection
on  RayDetection.ray_code = Ray.Ray_code
inner join Detection
on det_id = Detection_id
inner join Patient
on Id = patient_id 



where  [ray_checked]='N' and Detection_id = '3' 