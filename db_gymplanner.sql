CREATE DATABASE IF NOT EXISTS gymplanner;
USE gymplanner;

-- 1. Roles
CREATE TABLE roles (
  id   INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO roles (name) VALUES ('admin'), ('user');

-- 2. Users
CREATE TABLE users (
  id             INT AUTO_INCREMENT PRIMARY KEY,
  username       VARCHAR(50)  NOT NULL UNIQUE,
  password_hash  VARCHAR(255) NOT NULL,
  email          VARCHAR(100) NOT NULL UNIQUE,
  role_id        INT          NOT NULL DEFAULT 2,
  created_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
  updated_at     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP 
                       ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (role_id) REFERENCES roles(id)
);

-- 3. Goals & Fitness Levels
CREATE TABLE goals (
  id   INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO goals (name) VALUES
  ('endurance (cardio)'),
  ('strength'),
  ('hypertrophy (muscle growth)'),
  ('maintenance'),
  ('general fitness'),
  ('flexibility/mobility');

CREATE TABLE fitness_levels (
  id   INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO fitness_levels (name) VALUES ('beginner'), ('intermediate'), ('advanced');

-- 4. User Preferences
CREATE TABLE user_preferences (
  id                       INT AUTO_INCREMENT PRIMARY KEY,
  user_id                  INT NOT NULL,
  goal_id                  INT NOT NULL,
  level_id                 INT NOT NULL,
  available_days_per_week  INT NOT NULL,
  session_duration_minutes INT NOT NULL,
  created_at               DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id)  REFERENCES users(id),
  FOREIGN KEY (goal_id)  REFERENCES goals(id),
  FOREIGN KEY (level_id) REFERENCES fitness_levels(id)
);

-- 5. Muscle Groups
CREATE TABLE muscle_groups (
  id   INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(100) NOT NULL UNIQUE
);
INSERT INTO muscle_groups (name) VALUES
  ('full body'),('upper body'),('lower body'),('core/abs'),
  ('chest'),('back'),('shoulders'),('biceps'),('triceps'),
  ('quadriceps'),('hamstrings'),('glutes'),('calves'),
  ('forearms'),('hip adductors'),('hip abductors');

CREATE TABLE preference_muscle_groups (
  preference_id   INT NOT NULL,
  muscle_group_id INT NOT NULL,
  PRIMARY KEY (preference_id, muscle_group_id),
  FOREIGN KEY (preference_id)   REFERENCES user_preferences(id) ON DELETE CASCADE,
  FOREIGN KEY (muscle_group_id) REFERENCES muscle_groups(id)        ON DELETE CASCADE
);

-- 6. Difficulty Levels & Exercises
CREATE TABLE difficulty_levels (
  id   INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL UNIQUE
);
INSERT INTO difficulty_levels (name) VALUES ('beginner'), ('intermediate'), ('advanced');

CREATE TABLE exercises (
  id             INT AUTO_INCREMENT PRIMARY KEY,
  name           VARCHAR(100) NOT NULL UNIQUE,
  description    TEXT,
  difficulty_id  INT NOT NULL,
  media_url      VARCHAR(255),
  FOREIGN KEY (difficulty_id) REFERENCES difficulty_levels(id)
);

CREATE TABLE exercise_muscle_groups (
  exercise_id     INT NOT NULL,
  muscle_group_id INT NOT NULL,
  PRIMARY KEY (exercise_id, muscle_group_id),
  FOREIGN KEY (exercise_id)     REFERENCES exercises(id)     ON DELETE CASCADE,
  FOREIGN KEY (muscle_group_id) REFERENCES muscle_groups(id) ON DELETE CASCADE
);


-- 7. Schedules
CREATE TABLE schedules (
  id         INT AUTO_INCREMENT PRIMARY KEY,
  user_id    INT NOT NULL,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id) REFERENCES users(id)
);

-- 8. Days Lookup
CREATE TABLE days_of_week (
  id         TINYINT PRIMARY KEY,
  name       VARCHAR(9) NOT NULL,
  sort_order TINYINT NOT NULL
);
INSERT INTO days_of_week (id,name,sort_order) VALUES
  (1,'Monday',1),(2,'Tuesday',2),(3,'Wednesday',3),
  (4,'Thursday',4),(5,'Friday',5),(6,'Saturday',6),(7,'Sunday',7);

-- 9. Schedule Days (Option 1 surrogate PK)
CREATE TABLE schedule_days (
  id              INT AUTO_INCREMENT PRIMARY KEY,
  schedule_id     INT NOT NULL,
  day_of_week_id  TINYINT NOT NULL,
  UNIQUE KEY (schedule_id, day_of_week_id),
  FOREIGN KEY (schedule_id)    REFERENCES schedules(id)    ON DELETE CASCADE,
  FOREIGN KEY (day_of_week_id) REFERENCES days_of_week(id) ON DELETE RESTRICT
);

-- 10. Schedule Items
CREATE TABLE schedule_items (
  id               INT AUTO_INCREMENT PRIMARY KEY,
  schedule_day_id  INT NOT NULL,
  exercise_id      INT NOT NULL,
  sets             INT NOT NULL,
  reps             INT NOT NULL,
  FOREIGN KEY (schedule_day_id)
    REFERENCES schedule_days(id) ON DELETE CASCADE,
  FOREIGN KEY (exercise_id)
    REFERENCES exercises(id)
);

