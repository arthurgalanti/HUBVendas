INSERT INTO tb_category (category_id, category_name, category_description, created_on, fl_active, fl_removed)
VALUES
    ('08ee26d3-4a1c-4ae8-8407-f630daaada62', 'Eletrônicos', 'Categoria de produtos eletrônicos', NOW(), 1, 0),
    ('2a0d07da-4a30-461e-9e0c-ad18152f2ba2', 'Roupas', 'Categoria de produtos de vestuário', NOW(), 1, 0),
    ('37ea738f-d1e7-4021-97dc-1ea6bdb03759', 'Móveis', 'Categoria de produtos de móveis', NOW(), 1, 0),
    ('ec08102e-74fb-41aa-b82b-afb7cad7297a', 'Alimentos', 'Categoria de produtos alimentícios', NOW(), 1, 0),
    ('31c738b8-0563-48f8-a2c3-a9c07eafd3bf', 'Jogos', 'Categoria de jogos eletrônicos', NOW(), 1, 0),
    ('a2e73514-592e-4c90-9363-f886a01fde47', 'Acessórios', 'Categoria de acessórios para eletrônicos', NOW(), 0, 1);
    
INSERT INTO tb_product (product_id, product_name, product_description, unit_price, quantity, created_on, fl_active, fl_removed, image_name, image_type, image_base64, category_id)
VALUES
    ('acf82551-4f38-49c8-aa1d-da133bacc48a', 'Smartphone', 'Um smartphone de última geração', 599.99, 100, NOW(), 1, 0, 'smartphone.jpg', 'jpeg', 'base64_encoded_image_data', '08ee26d3-4a1c-4ae8-8407-f630daaada62'),
    ('5814f367-113d-4a7a-abea-574e3e2a4df7', 'Notebook', 'Um notebook de alta performance', 899.99, 50, NOW(), 1, 0, 'notebook.jpg', 'jpeg', 'base64_encoded_image_data', '08ee26d3-4a1c-4ae8-8407-f630daaada62'),
    ('4704bde6-3624-4311-9f4b-3e9c747ce4d7', 'Camiseta', 'Uma camiseta confortável', 19.99, 200, NOW(), 1, 0, 'camiseta.jpg', 'jpeg', 'base64_encoded_image_data', '2a0d07da-4a30-461e-9e0c-ad18152f2ba2'),
    ('97ce7814-ae2d-43b4-afe9-76b2b11a6c7e', 'Calça Jeans', 'Uma calça jeans de qualidade', 29.99, 150, NOW(), 1, 0, 'calca_jeans.jpg', 'jpeg', 'base64_encoded_image_data', '2a0d07da-4a30-461e-9e0c-ad18152f2ba2'),
    ('3212d186-3a73-4d78-b6d4-97ea22d7cb78', 'Sofá', 'Um sofá moderno para sua sala', 499.99, 30, NOW(), 1, 0, 'sofa.jpg', 'jpeg', 'base64_encoded_image_data', '37ea738f-d1e7-4021-97dc-1ea6bdb03759'),
    ('0aa2e23b-5204-4ea3-bd8e-4cd0b09df8ff', 'Cama de Casal', 'Uma cama de casal confortável', 349.99, 40, NOW(), 1, 0, 'cama_casal.jpg', 'jpeg', 'base64_encoded_image_data', '37ea738f-d1e7-4021-97dc-1ea6bdb03759'),
    ('96c010f2-d65a-424b-ac3d-d5cea35b59ac', 'Arroz', 'Arroz de qualidade premium', 5.99, 500, NOW(), 1, 0, 'arroz.jpg', 'jpeg', 'base64_encoded_image_data', 'ec08102e-74fb-41aa-b82b-afb7cad7297a'),
    ('87455fc7-2ee0-438f-b40e-67f56515de7b', 'Feijão', 'Feijão preto de alta qualidade', 3.99, 300, NOW(), 1, 0, 'feijao.jpg', 'jpeg', 'base64_encoded_image_data', 'ec08102e-74fb-41aa-b82b-afb7cad7297a'),
    ('a1dae7ed-c016-4e7c-8b10-7b3d3292ccef', 'Controle de Xbox', 'Um controle para Xbox', 49.99, 50, NOW(), 1, 0, 'controle_xbox.jpg', 'jpeg', 'base64_encoded_image_data', '31c738b8-0563-48f8-a2c3-a9c07eafd3bf'),
    ('c9681cd8-848a-4926-bc20-dedf2726c587', 'Fone de Ouvido', 'Fone de ouvido de alta qualidade', 29.99, 100, NOW(), 0, 1, 'fone_ouvido.jpg', 'jpeg', 'base64_encoded_image_data', '31c738b8-0563-48f8-a2c3-a9c07eafd3bf'),
    ('3a0f3213-c66d-4261-9446-062fcf43cce4', 'Capa para Smartphone', 'Capa protetora para smartphone', 9.99, 200, NOW(), 1, 0, 'capa_smartphone.jpg', 'jpeg', 'base64_encoded_image_data', 'a2e73514-592e-4c90-9363-f886a01fde47'),
    ('43d45b39-75b4-4364-b71e-a6105694ab6f', 'Mouse sem Fio', 'Mouse sem fio para computador', 19.99, 150, NOW(), 1, 0, 'mouse_sem_fio.jpg', 'jpeg', 'base64_encoded_image_data', 'a2e73514-592e-4c90-9363-f886a01fde47');
    