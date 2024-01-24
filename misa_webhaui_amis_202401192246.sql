﻿--
-- Script was generated by Devart dbForge Studio for MySQL, Version 10.0.60.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 19/1/2024 10:46:56 PM
-- Server version: 10.5.23
--

--
-- Disable foreign keys
--
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

--
-- Set SQL mode
--
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

--
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE misa_webhaui_amis;

--
-- Drop table `employee`
--
DROP TABLE IF EXISTS employee;

--
-- Drop table `department`
--
DROP TABLE IF EXISTS department;

--
-- Set default database
--
USE misa_webhaui_amis;

--
-- Create table `department`
--
CREATE TABLE department (
  DepartmentId char(36) NOT NULL COMMENT 'khóa chính',
  DepartmentCode varchar(20) NOT NULL COMMENT 'mã phòng ban',
  DepartmentName varchar(255) NOT NULL COMMENT 'tên phòng ban',
  CreateDate date DEFAULT NULL COMMENT 'ngày tạo ',
  CreateBy varchar(255) DEFAULT NULL COMMENT 'người tạo',
  ModifileDate date DEFAULT NULL COMMENT 'ngày chỉnh sửa gần nhất',
  ModifileBy varbinary(255) DEFAULT NULL COMMENT 'người chỉnh sửa gần nhất',
  PRIMARY KEY (DepartmentId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 1820,
CHARACTER SET utf8mb3,
COLLATE utf8mb3_general_ci,
COMMENT = 'phòng ban ',
ROW_FORMAT = DYNAMIC;

--
-- Create table `employee`
--
CREATE TABLE employee (
  EmployeeId char(36) NOT NULL COMMENT 'khóa chính',
  EmployeeCode varchar(20) NOT NULL COMMENT 'mã nhân viên',
  EmployeeName varchar(100) NOT NULL COMMENT 'tên nhân viên',
  Gender int(11) DEFAULT NULL COMMENT 'giới tính 0 nữ , 1 nam,2 chưa xác định',
  IdentityCode varchar(25) DEFAULT NULL COMMENT 'số chứng minh nhân dân',
  IdentityDate date DEFAULT NULL COMMENT 'ngày cấp',
  `Position` varchar(100) DEFAULT NULL COMMENT 'chức danh',
  IdentityPlace varchar(255) DEFAULT NULL COMMENT 'Nơi cấp',
  Address varchar(255) DEFAULT NULL COMMENT 'địa chỉ',
  PhoneNumber varchar(25) DEFAULT NULL COMMENT 'điện thoại di động',
  LandlinePhone varchar(25) DEFAULT NULL COMMENT 'điện thoại cố định',
  Email varchar(100) DEFAULT NULL COMMENT 'địa chỉ email',
  BankAccount varchar(25) DEFAULT NULL COMMENT 'tài khoản ngân hàng',
  BankName varchar(255) DEFAULT NULL COMMENT 'Tên ngân hàng',
  Branch varchar(255) DEFAULT NULL COMMENT 'chi nhánh',
  CreateDate date DEFAULT NULL COMMENT 'ngày tạo',
  CreateBy varchar(255) DEFAULT NULL COMMENT 'người tạo',
  ModifileDate date DEFAULT NULL COMMENT 'ngày chỉnh sửa gần nhất',
  ModifileBy varchar(255) DEFAULT NULL COMMENT 'Người chỉnh sửa gần nhất',
  DepartmentId char(36) NOT NULL COMMENT 'khóa ngoài kết nối với bảng phòng ban',
  PRIMARY KEY (EmployeeId, DepartmentId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 4096,
CHARACTER SET utf8mb3,
COLLATE utf8mb3_general_ci,
COMMENT = 'Danh mục nhân viên',
ROW_FORMAT = DYNAMIC;

--
-- Create foreign key
--
ALTER TABLE employee
ADD CONSTRAINT FK_employee_department FOREIGN KEY (DepartmentId)
REFERENCES department (DepartmentId) ON DELETE NO ACTION;

-- 
-- Dumping data for table department
--
INSERT INTO department VALUES
('20595ff5-b6e0-11ee-9729-f8477b936e6f', 'PB02', 'Phòng nhân sự', NULL, NULL, NULL, NULL),
('205a5f6a-b6e0-11ee-9729-f8477b936e6f', 'PB03', 'Phòng kế toán', NULL, NULL, NULL, NULL),
('205aa1b3-b6e0-11ee-9729-f8477b936e6f', 'PB04', 'Phòng đào tạo', NULL, NULL, NULL, NULL),
('9feafdd0-b6e0-11ee-9729-f8477b936e6f', 'PB01', 'Phòng hành chính', NULL, NULL, NULL, NULL),
('9febd839-b6e0-11ee-9729-f8477b936e6f', 'PB08', 'Phòng bảo vệ', NULL, NULL, NULL, NULL),
('9fec0b31-b6e0-11ee-9729-f8477b936e6f', 'PB07', 'Phòng lễ tấn', NULL, NULL, NULL, NULL),
('9fec44ae-b6e0-11ee-9729-f8477b936e6f', 'PB06', 'Phòng an ninh', NULL, NULL, NULL, NULL),
('b7b8c0f7-b6dd-11ee-9729-f8477b936e6f', 'PB05', 'Phòng chăm sóc khách hàng', NULL, NULL, NULL, NULL);

-- 
-- Dumping data for table employee
--
INSERT INTO employee VALUES
('485918bf-b6e1-11ee-9729-f8477b936e6f', 'NV03', 'Nguyễn Thị Hoài', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'b7b8c0f7-b6dd-11ee-9729-f8477b936e6f'),
('48595ec3-b6e1-11ee-9729-f8477b936e6f', 'NV04', 'Đặng Trường Khánh', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '9febd839-b6e0-11ee-9729-f8477b936e6f'),
('9cabbe59-b6e0-11ee-9729-f8477b936e6f', 'NV01', 'Nguyễn Văn Anh', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'b7b8c0f7-b6dd-11ee-9729-f8477b936e6f'),
('9fec8264-b6e0-11ee-9729-f8477b936e6f', 'NV02', 'Nguyễn Văn Bằng', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'b7b8c0f7-b6dd-11ee-9729-f8477b936e6f');

--
-- Restore previous SQL mode
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

--
-- Enable foreign keys
--
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;