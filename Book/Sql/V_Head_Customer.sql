CREATE VIEW [dbo].[V_Head_Customer]
	AS 
select  T_Stock_OutHead.Creater,
	    T_Stock_OutHead.CreateTime,
		T_Stock_OutHead.Id,
		T_Stock_OutHead.CustomerId,
		T_Base_Customer.[Address],
		T_Base_Customer.Name,
		T_Base_Customer.Phone

from T_Stock_OutHead

 left join t_base_customer 
  on T_Stock_OutHead.CustomerId=t_base_customer.id