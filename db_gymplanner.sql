CREATE DATABASE IF NOT EXISTS gymplanner;
USE gymplanner;

-- 2. Roles (to distinguish admins vs. normal users)
CREATE TABLE roles (
  id      INT AUTO_INCREMENT PRIMARY KEY,
  name    VARCHAR(50) NOT NULL UNIQUE
);

-- Preload two roles: admin and user
INSERT INTO roles (name) VALUES ('admin'), ('user');

-- 3. Users
CREATE TABLE users (
  id             INT AUTO_INCREMENT PRIMARY KEY,
  username       VARCHAR(50)  NOT NULL UNIQUE,
  password_hash  VARCHAR(255) NOT NULL,
  email          VARCHAR(100) NOT NULL UNIQUE,
  first_name     VARCHAR(50),
  last_name      VARCHAR(50),
  role_id        INT          NOT NULL DEFAULT 2,
  created_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP 
                      ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (role_id) REFERENCES roles(id)
);

-- 4. Reference tables for user inputs
CREATE TABLE goals (
  id    INT AUTO_INCREMENT PRIMARY KEY,
  name  VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO goals (name) VALUES 
  ('conditie'), ('kracht'), ('combinatie');

CREATE TABLE fitness_levels (
  id    INT AUTO_INCREMENT PRIMARY KEY,
  name  VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO fitness_levels (name) VALUES 
  ('beginner'), ('intermediate'), ('advanced');

-- 5. Store each user’s preferences (answers at startup)
CREATE TABLE user_preferences (
  id                       INT AUTO_INCREMENT PRIMARY KEY,
  user_id                  INT NOT NULL,
  goal_id                  INT NOT NULL,
  level_id                 INT NOT NULL,
  available_days_per_week  INT NOT NULL,
  session_duration_minutes INT NOT NULL,
  wants_intensive          BOOLEAN DEFAULT FALSE,
  created_at               DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id)  REFERENCES users(id),
  FOREIGN KEY (goal_id)  REFERENCES goals(id),
  FOREIGN KEY (level_id) REFERENCES fitness_levels(id)
);

-- 6. Muscle groups (for exercises and user focus)
CREATE TABLE muscle_groups (
  id   INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(100) NOT NULL UNIQUE
);

-- Link user preferences to muscle groups of interest
CREATE TABLE preference_muscle_groups (
  preference_id     INT NOT NULL,
  muscle_group_id   INT NOT NULL,
  PRIMARY KEY (preference_id, muscle_group_id),
  FOREIGN KEY (preference_id)   REFERENCES user_preferences(id) ON DELETE CASCADE,
  FOREIGN KEY (muscle_group_id) REFERENCES muscle_groups(id)        ON DELETE CASCADE
);

-- 7. Exercise catalog
CREATE TABLE difficulty_levels (
  id   INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO difficulty_levels (name) VALUES ('easy'), ('medium'), ('hard');

CREATE TABLE exercises (
  id            INT AUTO_INCREMENT PRIMARY KEY,
  name          VARCHAR(100) NOT NULL UNIQUE,
  description   TEXT,
  difficulty_id INT          NOT NULL,
  instructions  TEXT,
  media_url     VARCHAR(255),
  FOREIGN KEY (difficulty_id) REFERENCES difficulty_levels(id)
);

-- Link exercises to muscle groups they target
CREATE TABLE exercise_muscle_groups (
  exercise_id     INT NOT NULL,
  muscle_group_id INT NOT NULL,
  PRIMARY KEY (exercise_id, muscle_group_id),
  FOREIGN KEY (exercise_id)     REFERENCES exercises(id)     ON DELETE CASCADE,
  FOREIGN KEY (muscle_group_id) REFERENCES muscle_groups(id) ON DELETE CASCADE
);

-- 8. Generated weekly schedules
CREATE TABLE schedules (
  id         INT AUTO_INCREMENT PRIMARY KEY,
  user_id    INT NOT NULL,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id) REFERENCES users(id)
);

-- Days within a schedule
CREATE TABLE schedule_days (
  id          INT AUTO_INCREMENT PRIMARY KEY,
  schedule_id INT NOT NULL,
  day_of_week ENUM(
    'Monday','Tuesday','Wednesday','Thursday',
    'Friday','Saturday','Sunday'
  ) NOT NULL,
  FOREIGN KEY (schedule_id) REFERENCES schedules(id) ON DELETE CASCADE
);

-- Exercises assigned to each day (with sets & reps)
CREATE TABLE schedule_items (
  id              INT AUTO_INCREMENT PRIMARY KEY,
  schedule_day_id INT NOT NULL,
  exercise_id     INT NOT NULL,
  sets            INT NOT NULL,
  reps            INT NOT NULL,
  FOREIGN KEY (schedule_day_id) REFERENCES schedule_days(id) ON DELETE CASCADE,
  FOREIGN KEY (exercise_id)     REFERENCES exercises(id)
);
