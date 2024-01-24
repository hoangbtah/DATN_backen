﻿namespace web_AMIS.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int? Gender { get; set; }
        public string GenderName 
        { 
            get { 
                switch(Gender)
                {
                    case 0:
                        return "Nữ";
                    case 1:
                        return "Nam";
                    default:
                        return "không xác định";
                }
            
            } 
        }
        public string? IdentityCode { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string? Position { get; set; }
        public string? IdentityPlace { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LandlinePhone { get; set; }
        public string? Email { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? Branch { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifileDate { get; set; }

        public string? ModifileBy { get; set; }

        public Guid DepartmentId { get; set; }


    }
}