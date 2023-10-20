DROP DATABASE IF EXISTS hubvendas;
CREATE DATABASE IF NOT EXISTS hubvendas;
USE hubvendas;

CREATE TABLE IF NOT EXISTS tb_categories (
    id CHAR(36) PRIMARY KEY,
    category_name VARCHAR(255) NOT NULL,
    category_description TEXT,
    created_on DATETIME NOT NULL,
	fl_active BOOL NOT NULL
);

CREATE TABLE IF NOT EXISTS tb_products (
    id CHAR(36) PRIMARY KEY,
    product_name VARCHAR(255) NOT NULL,
    product_description TEXT,
    sku TEXT,
    bar_code TEXT,
    cost_price DECIMAL(10, 2) NOT NULL,
	selling_price DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL,
    created_on DATETIME NOT NULL,
	fl_active BOOL NOT NULL,
	image_name VARCHAR(128),
	image_type VARCHAR(20),
	image_base64 LONGTEXT,
	category_id CHAR(36) NOT NULL,
    CONSTRAINT fk_product_category_id FOREIGN KEY (category_id)
    REFERENCES tb_categories (id)	
);
