CREATE DATABASE login_app1
USE login_app1

CREATE TABLE user_info(
	id_user int IDENTITY(1,1) PRIMARY KEY,
	name_user char(20) UNIQUE NOT NULL,
	password_user char(20) NOT NULL);
