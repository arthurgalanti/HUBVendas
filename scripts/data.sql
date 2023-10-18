INSERT INTO tb_category (category_id, category_name, category_description, created_on, fl_active, fl_removed)
VALUES
    ('f4e6d9e2-87a1-4ac7-8447-3f1753c91e55', 'Tecnologia', 'Produtos eletrônicos e inovações tecnológicas', NOW(), TRUE, FALSE),
    ('d1db4bb6-9f1f-4a9e-bc9f-21a2a4e7e33e', 'Moda', 'Roupas, acessórios e tendências de moda', NOW(), TRUE, FALSE),
    ('774dd582-82a9-4525-90f3-8b2cb730551c', 'Esportes', 'Equipamentos e atividades esportivas', NOW(), FALSE, FALSE),
    ('c891b3ea-1f6a-4d1d-b4f4-58b4b1c99a2f', 'Comida', 'Alimentos e bebidas', NOW(), FALSE, TRUE),
    ('12e24be5-3ac7-49a6-9c82-f88a40f3e28a', 'Saúde', 'Produtos e serviços de saúde', NOW(), FALSE, TRUE),
    ('a0a5a46b-7b8f-437b-b779-84e2457e2ed5', 'Lazer', 'Atividades recreativas e entretenimento', NOW(), TRUE, FALSE),
    ('2e3d7e8d-1630-4d4a-8c4b-6d9abf5e71f3', 'Automóveis', 'Veículos e acessórios automotivos', NOW(), TRUE, FALSE),
    ('b8f1c0c0-b695-4d60-bf4d-689e04c3f47d', 'Viagem', 'Destinos e serviços de viagem', NOW(), FALSE, FALSE),
    ('89f2eb04-0d49-4bf3-8f08-28c1655a51d9', 'Decoração', 'Decoração de interiores e design', NOW(), TRUE, FALSE),
    ('4e3440d3-3f44-4870-8fe7-1863b5ce9570', 'Livros', 'Livros e literatura', NOW(), TRUE, FALSE);
    
INSERT INTO tb_product (product_id, product_name, product_description, sku, bar_code, cost_price, selling_price, stock, created_on, fl_active, fl_removed, image_name, image_type, image_base64, category_id)
VALUES
    ('4fa1f0db-9e4f-4f52-8a54-7f75b10a555a', 'Smartphone', 'Um smartphone avançado com muitos recursos', 'SMRT01', '1234567890', 500.00, 699.99, 100, NOW(), TRUE, FALSE, 'smartphone.jpg', 'jpg', 'base64_image_data', 'f4e6d9e2-87a1-4ac7-8447-3f1753c91e55'),
    ('7f7d2c6f-0dbb-472f-8d22-c17ab62f8941', 'Camiseta', 'Uma camiseta de algodão confortável', 'CMT01', '9876543210', 10.00, 19.99, 200, NOW(), TRUE, FALSE, 'camiseta.jpg', 'jpg', 'base64_image_data', 'd1db4bb6-9f1f-4a9e-bc9f-21a2a4e7e33e'),
    ('641e4b15-f0a3-4c03-94e6-c08ce9ce69a9', 'Bola de Futebol', 'Uma bola de futebol de alta qualidade', 'FBL01', '4567890123', 15.00, 29.99, 50, NOW(), TRUE, FALSE, 'bola_futebol.jpg', 'jpg', 'base64_image_data', '774dd582-82a9-4525-90f3-8b2cb730551c'),
    ('d5d7b6f1-b761-4f45-a3e7-541442a52f59', 'Pizza Margherita', 'Uma deliciosa pizza de queijo e tomate', 'PZZ01', '5678901234', 8.00, 14.99, 30, NOW(), TRUE, FALSE, 'pizza_margherita.jpg', 'jpg', 'base64_image_data', 'c891b3ea-1f6a-4d1d-b4f4-58b4b1c99a2f'),
    ('adef8ce1-8bca-4f1a-944d-e774c5c7a789', 'Termômetro Digital', 'Um termômetro digital para medição de temperatura', 'THERM01', '6789012345', 5.00, 9.99, 80, NOW(), TRUE, FALSE, 'termometro.jpg', 'jpg', 'base64_image_data', '12e24be5-3ac7-49a6-9c82-f88a40f3e28a'),
    ('982cd7d4-44d0-41a7-8465-83a405e24c75', 'Cadeira de Praia', 'Uma cadeira de praia confortável e dobrável', 'BEACH01', '7890123456', 25.00, 49.99, 40, NOW(), TRUE, FALSE, 'cadeira_praia.jpg', 'jpg', 'base64_image_data', 'a0a5a46b-7b8f-437b-b779-84e2457e2ed5'),
    ('7a9eb0ed-2329-49fc-8b20-611241c079d2', 'Pneu de Carro', 'Um pneu de carro durável para estradas', 'TIRE01', '8901234567', 50.00, 79.99, 60, NOW(), TRUE, FALSE, 'pneu_carro.jpg', 'jpg', 'base64_image_data', '2e3d7e8d-1630-4d4a-8c4b-6d9abf5e71f3'),
    ('5c3d6743-6aae-4aa5-8315-bbf1c7b0db53', 'Passagem Aérea', 'Uma passagem de avião para um destino exótico', 'FLIGHT01', '9012345678', 300.00, 499.99, 20, NOW(), TRUE, FALSE, 'passagem_aerea.jpg', 'jpg', 'base64_image_data', 'b8f1c0c0-b695-4d60-bf4d-689e04c3f47d'),
    ('2e6e0ec7-42b7-47db-8c09-c8c92e5f9357', 'Luminária de Mesa', 'Uma luminária elegante para sua mesa', 'LAMP01', '0123456789', 20.00, 39.99, 90, NOW(), TRUE, FALSE, 'luminaria.jpg', 'jpg', 'base64_image_data', '89f2eb04-0d49-4bf3-8f08-28c1655a51d9'),
    ('60b8a00f-8321-48c7-87fc-8e3b8c62b3e2', 'Livro de Ficção', 'Um emocionante livro de ficção para leitura', 'BOOK01', '3456789012', 12.00, 24.99, 150, NOW(), TRUE, FALSE, 'livro_ficcao.jpg', 'jpg', 'base64_image_data', '4e3440d3-3f44-4870-8fe7-1863b5ce9570'),
    ('ae6e5bca-2a7f-4d05-bd17-6c126f0c7bcf', 'Mouse Óptico', 'Um mouse óptico de alta precisão', 'MOUSE01', '1234567890', 8.00, 14.99, 50, NOW(), FALSE, FALSE, 'mouse_optico.jpg', 'jpg', 'base64_image_data', 'f4e6d9e2-87a1-4ac7-8447-3f1753c91e55'),
    ('b54094b7-78a0-4d45-b28d-849b68f2179e', 'Chapéu de Sol', 'Um chapéu de sol para proteção contra o sol', 'SUNHAT01', '2345678901', 10.00, 19.99, 30, NOW(), FALSE, FALSE, 'chapeu_sol.jpg', 'jpg', 'base64_image_data', 'a0a5a46b-7b8f-437b-b779-84e2457e2ed5'),
    ('edeac4c1-50cd-4d3e-8ed1-0be88e4a7c81', 'Mochila Esportiva', 'Uma mochila resistente para atividades esportivas', 'SPORTBAG01', '3456789012', 25.00, 49.99, 20, NOW(), FALSE, FALSE, 'mochila_esportiva.jpg', 'jpg', 'base64_image_data', '774dd582-82a9-4525-90f3-8b2cb730551c'),
    ('d99eac3d-5d06-4c79-96f0-61e4e3f6f59a', 'Fones de Ouvido', 'Fones de ouvido com cancelamento de ruído', 'HEADPHONES01', '4567890123', 40.00, 79.99, 15, NOW(), FALSE, FALSE, 'fones_ouvido.jpg', 'jpg', 'base64_image_data', '2e3d7e8d-1630-4d4a-8c4b-6d9abf5e71f3'),
    ('f073102a-6f6f-4325-8d3b-4c59c1b4e0d0', 'Câmera Digital', 'Uma câmera digital de alta resolução', 'CAMERA01', '5678901234', 120.00, 249.99, 10, NOW(), FALSE, FALSE, 'camera_digital.jpg', 'jpg', 'base64_image_data', '4e3440d3-3f44-4870-8fe7-1863b5ce9570'),
    ('bc104f8f-94c9-4d1e-9400-c5f5d9e8a461', 'Teclado Mecânico', 'Um teclado mecânico de alta qualidade', 'KEYBOARD01', '1234567890', 30.00, 59.99, 40, NOW(), FALSE, TRUE, 'teclado_mecanico.jpg', 'jpg', 'base64_image_data', 'f4e6d9e2-87a1-4ac7-8447-3f1753c91e55'),
    ('e8b4e13b-cf7d-4b08-b6a0-dc174b0ec8b7', 'Óculos de Sol', 'Óculos de sol elegantes e estilosos', 'SUNGLASSES01', '2345678901', 15.00, 29.99, 25, NOW(), FALSE, TRUE, 'oculos_sol.jpg', 'jpg', 'base64_image_data', 'a0a5a46b-7b8f-437b-b779-84e2457e2ed5'),
    ('6f21cb13-57e0-4d6f-8ee4-04d9b68e7c34', 'Raquete de Tênis', 'Uma raquete de tênis de alto desempenho', 'TENNISRACKET01', '3456789012', 40.00, 79.99, 15, NOW(), FALSE, TRUE, 'raquete_tenis.jpg', 'jpg', 'base64_image_data', '774dd582-82a9-4525-90f3-8b2cb730551c'),
    ('e0e4382d-bf58-43d9-91a9-03642893e84e', 'Caixa de Som Bluetooth', 'Caixa de som portátil com conectividade Bluetooth', 'BLUETOOTHSPEAKER01', '4567890123', 50.00, 99.99, 20, NOW(), FALSE, TRUE, 'caixa_som_bluetooth.jpg', 'jpg', 'base64_image_data', '2e3d7e8d-1630-4d4a-8c4b-6d9abf5e71f3'),
    ('0c8f8e24-1a0a-4ec4-aa3a-06c20c50888a', 'Tablet Android', 'Um tablet Android de última geração', 'TABLET01', '5678901234', 80.00, 149.99, 10, NOW(), FALSE, TRUE, 'tablet_android.jpg', 'jpg', 'base64_image_data', '4e3440d3-3f44-4870-8fe7-1863b5ce9570');