CREATE PROCEDURE GetByEmail(IN emailaddress VARCHAR(100))
 BEGIN
    SELECT  * 
    FROM    tmp_unittest_table
    WHERE   email = emailaddress;
 END