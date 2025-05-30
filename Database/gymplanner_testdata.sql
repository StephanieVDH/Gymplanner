-- TEST DATA

-- Admin account:
INSERT INTO users (username,password_hash,email,role_id)
VALUES (
  'AdminS',
  '$2a$12$9QTiK2GiGt1Vvl4ZuWD7qOUKmgSD82IKtsB7KkngoT6jYhN2XTAGu',
  'vanderhaegens@icloud.com',
  1
);

-- User accounts: (alle users hebben als password 'Wachtwoord1')
INSERT INTO users (username, password_hash, email) VALUES
  ('StephVDH',  '$2a$12$IwFdTBiUyMcfTg86J3wfluoK.exYE/Q0tMa2Kh37CqJEPj.XO/ENO', 'xxxstefiexxx@gmail.com'),
  ('JanJanssens', '$2a$12$IwFdTBiUyMcfTg86J3wfluoK.exYE/Q0tMa2Kh37CqJEPj.XO/ENO', 'jj@outlook.com'),
  ('Bertje123',   '$2a$12$IwFdTBiUyMcfTg86J3wfluoK.exYE/Q0tMa2Kh37CqJEPj.XO/ENO', 'bert@outlook.com'),
  ('Antonello',   '$2a$12$IwFdTBiUyMcfTg86J3wfluoK.exYE/Q0tMa2Kh37CqJEPj.XO/ENO', 'antonello.coppini@gmail.com'),
  ('Raia',        '$2a$12$IwFdTBiUyMcfTg86J3wfluoK.exYE/Q0tMa2Kh37CqJEPj.XO/ENO', 'raisa.h@gmail.com');



