CREATE DATABASE IntraManage;
GO

USE IntraManage;
GO


CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),  
    RoleName VARCHAR(100) NOT NULL     
);
GO

CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    DepartmentName VARCHAR(100) NOT NULL 
);
GO

CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1),   
    Name VARCHAR(100) NOT NULL,         
    Email VARCHAR(100) NOT NULL UNIQUE, 
    PasswordHash VARCHAR(255) NOT NULL,  
	Cedula VARCHAR(255) NOT NULL,
    RoleID INT,                          
    DepartmentID INT,                    
    FOREIGN KEY (RoleID) REFERENCES Roles(Id),  
    FOREIGN KEY (DepartmentID) REFERENCES Departments(Id)  
);

INSERT INTO Roles (RoleName) VALUES 
('Administrador'), 
('Basico');
GO

INSERT INTO Departments (DepartmentName) VALUES 
('Recursos Humanos'), 
('Tecnologia'), 
('Asistencia al usuario');
GO
