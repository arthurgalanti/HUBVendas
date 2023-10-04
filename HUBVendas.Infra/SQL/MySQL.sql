CREATE DATABASE IF NOT EXISTS HUBVendas;
USE HUBVendas;

CREATE TABLE IF NOT EXISTS tb_category (
    category_id CHAR(36) PRIMARY KEY,
    category_name VARCHAR(255) NOT NULL,
    category_description TEXT,
    created_on DATETIME NOT NULL,
	fl_active BOOL NOT NULL,
	fl_removed BOOL NOT NULL
);

CREATE TABLE IF NOT EXISTS tb_product (
    product_id CHAR(36) PRIMARY KEY,
    product_name VARCHAR(255) NOT NULL,
    product_description TEXT,
	unit_price DECIMAL(10, 2) NOT NULL,
    quantity INT NOT NULL,
    created_on DATETIME NOT NULL,
	fl_active BOOL NOT NULL,
	fl_removed BOOL NOT NULL,
	image_name VARCHAR(128),
	image_type VARCHAR(20),
	image_base64 TEXT,
	category_id CHAR(36) NOT NULL,
    CONSTRAINT fk_product_category_id FOREIGN KEY (category_id)
    REFERENCES tb_category (category_id)	
);

CREATE INDEX idx_product_category_id ON tb_product (category_id);
