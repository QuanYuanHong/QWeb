CREATE VIEW [dbo].[V_Head_Provider]
	AS 
select  T_Stock_InHead.Creater,
	    T_Stock_InHead.CreateTime,
		T_Stock_InHead.Id,
		T_Stock_InHead.ProviderId,
		T_Base_Provider.[Address],
		T_Base_Provider.Connector,
		T_Base_Provider.Email,
		T_Base_Provider.Fax,
		T_Base_Provider.Name,
		T_Base_Provider.Phone
  
FROM T_Stock_InHead left join T_Base_Provider on T_Stock_InHead.ProviderId=T_Base_Provider.Id