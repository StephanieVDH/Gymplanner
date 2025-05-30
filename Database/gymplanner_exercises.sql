-- BICEPS
-- 1. Barbell Curl (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Barbell Curl',
 'Stand upright holding a barbell with an underhand grip, curl the bar toward your shoulders under control, then lower back to start.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Barbell Curl';

-- 2. Kettlebell Concentration Curl (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Kettlebell Concentration Curl',
 'Seated with elbow braced against your inner thigh, curl a kettlebell toward your shoulder focusing on biceps contraction.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Kettlebell Concentration Curl';

-- 3. Kettlebell Preacher Curl (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Kettlebell Preacher Curl',
 'Using a preacher bench, hold a kettlebell with an underhand grip and curl it upward to isolate the biceps.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Kettlebell Preacher Curl';

-- 4. Kettlebell Single Arm Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Kettlebell Single Arm Curl',
 'Stand with feet hip-width apart and curl a kettlebell with one arm, keeping your elbow stationary at your side.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Kettlebell Single Arm Curl';

-- 5. Cable Twisting Curl (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Cable Twisting Curl',
 'Attach a straight bar to a low pulley and twist your wrists as you curl the weight toward your shoulders for full biceps contraction.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Cable Twisting Curl';

-- 6. Cable Single Arm Bayesian Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Cable Single Arm Bayesian Curl',
 'Using a single-handle attachment on a cable machine, curl one arm while stabilizing your body for isolated biceps work.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Cable Single Arm Bayesian Curl';

-- 7. Cable Single Arm Reverse Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Cable Single Arm Reverse Curl',
 'Attach a handle to a low pulley, grasp with an overhand grip, and curl the weight toward your shoulder to target forearms and biceps.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Cable Single Arm Reverse Curl';

-- 8. Cable Single Arm Hammer Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Cable Single Arm Hammer Curl',
 'With a neutral grip, curl a single cable handle upward keeping your wrist straight to target the brachialis and biceps.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Cable Single Arm Hammer Curl';

-- 9. Band Bayesian Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Band Bayesian Curl',
 'Anchor a resistance band under your feet and curl the handles upward with one arm, maintaining tension on the biceps.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Band Bayesian Curl';

-- 10. Band Bayesian Hammer Curl (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Band Bayesian Hammer Curl',
 'Stand on a resistance band with a neutral grip and curl upward focusing on the brachioradialis and biceps.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Band Bayesian Hammer Curl';

-- 11. Band Bayesian Reverse Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Band Bayesian Reverse Curl',
 'Grip a resistance band with an overhand hold and curl upward to engage forearms and biceps.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Band Bayesian Reverse Curl';

-- 12. Barbell Reverse Curl (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Barbell Reverse Curl',
 'Hold a barbell with an overhand grip and curl toward your shoulders, keeping elbows close to isolate forearms and biceps.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT id, 8 FROM exercises WHERE name = 'Barbell Reverse Curl';

-- TRICEPS
-- 1. Dumbbell Skull Crusher (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dumbbell Skull Crusher',
 'Lie on a flat bench holding dumbbells overhead with arms extended; bend at the elbows to lower the weights toward your temples, then extend back up.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Dumbbell Skull Crusher';

-- 2. Seated Arnold Press (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Seated Arnold Press',
 'Sit upright with dumbbells in front of shoulders, palms facing you; rotate palms forward and press overhead, then reverse on the way down.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Seated Arnold Press';

-- 3. Single Arm Press (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Single Arm Press',
 'Standing or seated, hold one dumbbell at shoulder level and press it straight overhead, keeping core tight and elbow tracking under wrist.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Single Arm Press';

-- 4. Overhead Tricep Extension (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Overhead Tricep Extension',
 'Hold a single dumbbell overhead with both hands, lower it behind your head by bending elbows, then raise back to start, keeping upper arms fixed.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Overhead Tricep Extension';

-- 5. Tricep Kickback (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Tricep Kickback',
 'Hinge at hips with a flat back, hold a dumbbell in one hand, extend your elbow to straighten the arm behind you, then return under control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Tricep Kickback';

-- 6. Weighted Dips (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Weighted Dips',
 'Using parallel bars, lower your body by bending elbows until upper arms are parallel to floor, then press back up; add weight via belt or vest.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Weighted Dips';

-- 7. Alternating Arnold Press (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Alternating Arnold Press',
 'Perform the Arnold press movement one arm at a time: rotate and press one dumbbell overhead, lower it while starting the opposite arm overhead.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Alternating Arnold Press';

-- 8. Guillotine Bench Press (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Guillotine Bench Press',
 'On a flat bench, grip the bar wider than shoulder-width, lower it to your neck/chin level under control, then press it back up, focusing on triceps drive.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Triceps'
WHERE e.name = 'Guillotine Bench Press';

-- SHOULDERS
-- 1. Dumbbell Rear Delt Fly (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dumbbell Rear Delt Fly',
 'Hinge at the hips with a flat back holding dumbbells, then lift weights out to the sides with a slight bend in the elbows to target the rear delts.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'shoulders'
WHERE e.name = 'Dumbbell Rear Delt Fly';

-- 2. Dumbbell Arnold Press (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dumbbell Arnold Press',
 'Seated or standing, start with palms facing you at shoulder height; rotate palms forward as you press overhead, then reverse on the descent.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'shoulders'
WHERE e.name = 'Dumbbell Arnold Press';

-- 3. Barbell Front Raise (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Barbell Front Raise',
 'Stand holding a barbell with an overhand grip in front of your thighs; raise it straight up to eye level, then lower under control to work the front delts.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'shoulders'
WHERE e.name = 'Barbell Front Raise';

-- 4. Barbell Overhead Press (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Barbell Overhead Press',
 'Rack a barbell at shoulder height, grip just outside shoulder width, then press it overhead until arms lock out, keeping core braced.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'shoulders'
WHERE e.name = 'Barbell Overhead Press';

-- 5. Landmine Lateral Raise (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Landmine Lateral Raise',
 'Stand side-on to a landmine attachment, hold the barbell end with one hand, and raise your arm laterally to shoulder height for a controlled lateral delt workout.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'shoulders'
WHERE e.name = 'Landmine Lateral Raise';

-- 6. Dumbbell Bent Arm Lateral Raise (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dumbbell Bent Arm Lateral Raise',
 'Stand holding dumbbells with elbows bent at 90°, then lift your forearms out to the sides—keeping the elbow angle fixed—to isolate the side delts.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'shoulders'
WHERE e.name = 'Dumbbell Bent Arm Lateral Raise';

-- BACK
-- LOWER BACK:
-- 1. Superman (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Superman',
 'Lie face down with arms extended overhead; simultaneously lift arms, chest, and legs off the floor, hold briefly, then lower with control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Superman';

-- 2. Bird Dog (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Bird Dog',
 'On hands and knees, extend opposite arm and leg in line with your torso, hold for a moment to engage the lower back, then switch sides.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Bird Dog';

-- 3. Hyperextension (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Hyperextension',
 'Position yourself on a hyperextension bench with hips supported; bend at the waist to lower torso, then extend back up until body is in line.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Hyperextension';

-- 4. Good Morning (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Good Morning',
 'With a barbell on your upper back, hinge at the hips keeping a slight knee bend; lower torso until parallel to the floor, then return upright.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Good Morning';

-- 5. Conventional Deadlift (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Conventional Deadlift',
 'Stand with feet under barbell, hinge at hips and knees to grip bar, then drive through heels to lift bar to hip level, keeping spine neutral.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Conventional Deadlift';

-- 6. Snatch Grip Deadlift (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Snatch Grip Deadlift',
 'Use a wide, snatch-style grip on the barbell; hinge at hips and knees to lift the bar, emphasizing lower back and posterior chain through full extension.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Snatch Grip Deadlift';

-- BACK: LATS
-- 1. Resistance Band Lat Pulldown (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Resistance Band Lat Pulldown',
 'Anchor a resistance band overhead, grasp the handles, and pull them down toward your chest, squeezing your lats at the bottom before returning under control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Resistance Band Lat Pulldown';

-- 2. Straight-Arm Cable Pulldown (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Straight-Arm Cable Pulldown',
 'Stand facing a high pulley, grasp a straight bar with arms extended, then pull the bar down to your thighs by driving your lats, keeping arms straight throughout.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Straight-Arm Cable Pulldown';

-- 3. Pull-Up (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Pull-Up',
 'Grasp an overhead bar with a shoulder-width overhand grip and pull your chin above the bar, focusing on pulling through your elbows and engaging your lats.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Pull-Up';

-- 4. Single-Arm Dumbbell Row (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Single-Arm Dumbbell Row',
 'Place one knee and the same-side hand on a bench, hinge forward with a flat back, and row a dumbbell to your hip, emphasizing lat contraction on the working side.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Single-Arm Dumbbell Row';

-- 5. T-Bar Row (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('T-Bar Row',
 'Stand over a T-bar row handle, hinge at the hips, grasp the handles, and row the weight to your sternum, driving through your lats and keeping your torso steady.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'T-Bar Row';

-- 6. Weighted Pull-Up (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Weighted Pull-Up',
 'Perform a pull-up with additional weight (via belt or vest), pulling until your chin clears the bar, then lower slowly to fully challenge your lats under load.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Weighted Pull-Up';

-- UPPER BACK:
-- 1. Band Pull-Apart (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Band Pull-Apart',
 'Stand with feet hip-width apart holding a resistance band in front of you at shoulder height with arms extended; pull the band apart by retracting your shoulder blades until your arms are out wide, then return under control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Band Pull-Apart';

-- 2. Face Pull (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Face Pull',
 'Attach a rope to a high pulley, grasp with an overhand grip and step back; pull the rope toward your face by squeezing your shoulder blades, then extend your arms back under control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Face Pull';

-- 3. Seated Cable Row (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Seated Cable Row',
 'Sit at a cable row machine with knees slightly bent, grasp the handle, then pull it toward your torso while keeping your back flat and squeezing your shoulder blades, finally extending your arms back.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Seated Cable Row';

-- 4. Bent-Over Barbell Row (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Bent-Over Barbell Row',
 'Stand holding a barbell with an overhand grip, hinge at your hips until your torso is about 45° to the floor; pull the barbell toward your lower chest by retracting your shoulder blades, then lower under control.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Back'
WHERE e.name = 'Bent-Over Barbell Row';


-- CHEST
-- 1. Push-Up (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Push-Up',
 'Start in a high plank with hands under shoulders; lower your body until your chest nearly touches the floor, then press back up to full extension.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Chest'
WHERE e.name = 'Push-Up';

-- 2. Dumbbell Floor Press (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dumbbell Floor Press',
 'Lie on the floor holding dumbbells at your sides; press them up until arms are straight, then lower until your upper arms touch the floor.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Chest'
WHERE e.name = 'Dumbbell Floor Press';

-- 3. Incline Dumbbell Press (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Incline Dumbbell Press',
 'Set an bench to a 30–45° incline; press dumbbells from chest level up and together, focusing on upper-chest contraction, then lower under control.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Chest'
WHERE e.name = 'Incline Dumbbell Press';

-- 4. Chest Dips (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Chest Dips',
 'Using parallel bars, lean slightly forward and lower your body by bending elbows until your shoulders dip below elbows, then push back up.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Chest'
WHERE e.name = 'Chest Dips';

-- 5. Barbell Bench Press (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Barbell Bench Press',
 'Lie on a flat bench with feet planted; grip the barbell just outside shoulder width, lower it to mid-chest, then press back up explosively.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Chest'
WHERE e.name = 'Barbell Bench Press';

-- 6. Single-Arm Dumbbell Bench Press (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Single-Arm Dumbbell Bench Press',
 'Lie on a bench and press one dumbbell overhead while keeping your torso stable; lower under control and repeat on the other side to challenge core stability.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Chest'
WHERE e.name = 'Single-Arm Dumbbell Bench Press';

-- QUADRICEPS:
-- Difficulty mapping: beginner = 1, intermediate = 2, advanced = 3
-- Muscle group lookup by name: 'Quadriceps'

-- 1. Bodyweight Squat (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Bodyweight Squat',
 'Stand with feet shoulder-width apart and squat down by pushing hips back and bending knees, keeping chest up; drive through heels to return to standing.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Quadriceps'
WHERE e.name = 'Bodyweight Squat';

-- 2. Goblet Squat (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Goblet Squat',
 'Hold a dumbbell or kettlebell at chest height, squat down by bending knees and pushing hips back, then stand up by extending through the quads.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Quadriceps'
WHERE e.name = 'Goblet Squat';

-- 3. Bulgarian Split Squat (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Bulgarian Split Squat',
 'Place rear foot on a bench behind you, lower your front knee until thigh is parallel to floor, then drive through the front heel to return up.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Quadriceps'
WHERE e.name = 'Bulgarian Split Squat';

-- 4. Walking Lunges (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Walking Lunges',
 'Step forward into a lunge by lowering your back knee toward the floor, then push off the front foot to bring the rear leg forward into the next lunge.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Quadriceps'
WHERE e.name = 'Walking Lunges';

-- 5. Front Squat (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Front Squat',
 'Rack a barbell across the front of your shoulders, keep elbows high, squat down by bending hips and knees, then drive up through the quads to standing.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Quadriceps'
WHERE e.name = 'Front Squat';

-- 6. Back Squat (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Back Squat',
 'Position a barbell across your upper back, squat by pushing hips back and bending knees to lower until thighs are parallel, then extend knees to stand.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Quadriceps'
WHERE e.name = 'Back Squat';

-- HAMSTRINGS
-- 1. Glute Bridge (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Glute Bridge',
 'Lie on your back with knees bent and feet flat; drive through your heels to lift hips until body forms a straight line from shoulders to knees, squeezing hamstrings and glutes at the top.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hamstrings'
WHERE e.name = 'Glute Bridge';

-- 2. Stability Ball Hamstring Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Stability Ball Hamstring Curl',
 'Lie on your back with heels on a stability ball; lift hips and roll the ball toward you by bending knees, then extend legs to roll it back, keeping hips elevated.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hamstrings'
WHERE e.name = 'Stability Ball Hamstring Curl';

-- 3. Romanian Deadlift (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Romanian Deadlift',
 'Hold a barbell or dumbbells at hip height; hinge at the hips with a slight knee bend, lower weights along the legs until you feel a stretch in the hamstrings, then return to standing.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hamstrings'
WHERE e.name = 'Romanian Deadlift';

-- 4. Single-Leg Deadlift (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Single-Leg Deadlift',
 'Stand on one leg holding a weight in the opposite hand; hinge at the hip of the standing leg, lowering the weight toward the floor while extending the free leg back, then return upright.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hamstrings'
WHERE e.name = 'Single-Leg Deadlift';

-- 5. Glute-Ham Raise (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Glute-Ham Raise',
 'Position yourself on a glute-ham developer machine with feet anchored; lower your upper body forward under control by extending at the knees, then pull back up using hamstrings.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hamstrings'
WHERE e.name = 'Glute-Ham Raise';

-- 6. Nordic Hamstring Curl (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Nordic Hamstring Curl',
 'Kneel with ankles secured under a fixed object; lower your torso forward as slowly as possible, resisting with hamstrings, then push back up to the start position.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hamstrings'
WHERE e.name = 'Nordic Hamstring Curl';

-- GLUTES
-- 1. Glute Bridge (Beginner) — reuse existing exercise
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Glutes'
WHERE e.name = 'Glute Bridge';

-- 2. Donkey Kick (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Donkey Kick',
 'Start on all fours, kick one leg back and up toward the ceiling, squeezing your glute at the top, then lower under control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Glutes'
WHERE e.name = 'Donkey Kick';

-- 3. Single-Leg Deadlift (Intermediate) — reuse existing exercise
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Glutes'
WHERE e.name = 'Single-Leg Deadlift';

-- 4. Cable Pull-Through (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Cable Pull-Through',
 'Stand facing away from a low pulley with a rope attachment between your legs; hinge at the hips to pull the rope forward by contracting your glutes, then return under control.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Glutes'
WHERE e.name = 'Cable Pull-Through';

-- 5. Barbell Hip Thrust (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Barbell Hip Thrust',
 'Position your upper back on a bench with a barbell resting on your hips; drive through your heels to lift your hips until torso is parallel to the floor, squeezing glutes at the top.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Glutes'
WHERE e.name = 'Barbell Hip Thrust';

-- 6. Single-Leg Hip Thrust (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Single-Leg Hip Thrust',
 'Lie with your upper back on a bench and one foot on the floor; drive through the heel to lift your hips, keeping the other leg extended, and squeeze the glute at the top.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Glutes'
WHERE e.name = 'Single-Leg Hip Thrust';

-- CALVES:
-- 1. Standing Calf Raise (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Standing Calf Raise',
 'Stand on a raised platform or step with heels hanging off; lift your heels by contracting the calf muscles, pause at the top, then lower under control to stretch your calves.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Calves'
WHERE e.name = 'Standing Calf Raise';

-- 2. Seated Calf Raise (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Seated Calf Raise',
 'Sit on a bench or calf‐raise machine with weights resting on your knees; lift your heels by pressing through the balls of your feet, squeeze at the top, then lower until you feel a stretch.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Calves'
WHERE e.name = 'Seated Calf Raise';

-- 3. Donkey Calf Raise (Advanced)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Donkey Calf Raise',
 'Position yourself on a donkey calf‐raise machine (or lean forward with weight on your lower back); perform calf raises by lifting heels as high as possible, then lower heels below platform level under control.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Calves'
WHERE e.name = 'Donkey Calf Raise';

-- CORE / ABS:
-- 1. Plank
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Plank',
 'Begin in a forearm plank position with elbows under shoulders and body in a straight line; brace your core and hold without letting hips sag or hike.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Plank';

-- 2. Dead Bug
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dead Bug',
 'Lie on your back with arms extended toward ceiling and knees bent 90°; slowly lower opposite arm and leg toward the floor, then return and switch sides.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Dead Bug';

-- 3. Crunch
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Crunch',
 'Lie on your back with knees bent and feet flat; lift your shoulders off the floor by contracting your abs, pause briefly at the top, then lower back down.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Crunch';

-- 4. Russian Twist
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Russian Twist',
 'Sit with knees bent and feet off the floor, lean back slightly; rotate your torso side to side, tapping hands or a weight to the floor on each side.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Russian Twist';

-- 5. Hanging Knee Raise
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Hanging Knee Raise',
 'Hang from a pull-up bar with arms fully extended; curl your knees toward your chest by engaging your lower abs, then lower legs back down under control.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Hanging Knee Raise';

-- 6. Mountain Climbers
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Mountain Climbers',
 'Start in a high plank; drive one knee toward your chest, then quickly switch legs in a running motion while keeping hips level and core tight.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Mountain Climbers';

-- 7. Hanging Leg Raise
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Hanging Leg Raise',
 'Hang from a pull-up bar with legs straight; lift your legs up toward the bar by engaging lower abs, keeping legs as straight as possible, then lower slowly.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Hanging Leg Raise';

-- 8. Ab Wheel Rollout
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Ab Wheel Rollout',
 'Kneel holding an ab wheel; roll it forward, extending your body into a straight line, then pull back in by contracting your abs without letting your hips sag.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Ab Wheel Rollout';

-- 9. Dragon Flag
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dragon Flag',
 'Lie on a bench and grip behind your head; lift your entire body into a straight line from shoulders to feet, then slowly lower with control keeping core braced.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'core/abs'
WHERE e.name = 'Dragon Flag';

-- FOREARMS:
-- Difficulty mapping: beginner = 1, intermediate = 2, advanced = 3
-- Muscle group lookup by name: 'Forearms'

-- 1. Seated Wrist Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Seated Wrist Curl',
 'Sit on a bench holding a barbell or dumbbells with palms up and forearms resting on your thighs; curl your wrists upward, pause at the top, then lower under control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Forearms'
WHERE e.name = 'Seated Wrist Curl';

-- 2. Seated Reverse Wrist Curl (Beginner)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Seated Reverse Wrist Curl',
 'Sit on a bench holding a barbell or dumbbells with palms down and forearms on your thighs; lift the backs of your hands by extending the wrists, then lower with control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Forearms'
WHERE e.name = 'Seated Reverse Wrist Curl';

-- 3. Farmer\'s Walk (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Farmer''s Walk',
 'Hold a heavy dumbbell or kettlebell in each hand at your sides; walk a set distance while maintaining an upright posture and a firm grip.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Forearms'
WHERE e.name = 'Farmer''s Walk';

-- 4. Plate Pinch (Intermediate)
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Plate Pinch',
 'Grip two weight plates together smooth‐side out and pinch them between your fingers and thumb; hold for time to train finger and forearm strength.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Forearms'
WHERE e.name = 'Plate Pinch';

-- 5. Barbell Reverse Curl (Advanced) — reuse existing exercise
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Forearms'
WHERE e.name = 'Barbell Reverse Curl';

-- 6. Cable Single Arm Reverse Curl (Advanced) — reuse existing exercise
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Forearms'
WHERE e.name = 'Cable Single Arm Reverse Curl';

-- HIP ADDUCTION:
-- 1. Standing Band Hip Adduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Standing Band Hip Adduction',
 'Anchor a resistance band low to the side, loop it around one ankle and stand perpendicular to the anchor; pull the leg across your body against the band, squeezing the inner thigh, then return with control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Adductors'
WHERE e.name = 'Standing Band Hip Adduction';

-- 2. Side-Lying Hip Adduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Side-Lying Hip Adduction',
 'Lie on your side with the top leg bent and supported; lift the bottom leg upward towards the ceiling by contracting the inner thigh, then lower it back down slowly.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Adductors'
WHERE e.name = 'Side-Lying Hip Adduction';

-- 3. Seated Machine Hip Adduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Seated Machine Hip Adduction',
 'Sit on a hip-adduction machine with pads against the insides of your knees; squeeze the pads together by contracting your inner thighs, hold briefly, then release under control.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Adductors'
WHERE e.name = 'Seated Machine Hip Adduction';

-- 4. Cable Hip Adduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Cable Hip Adduction',
 'Attach an ankle cuff to a low pulley, stand sideways to the machine; pull the working leg across your body against cable resistance, focusing on the squeeze, then return slowly.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Adductors'
WHERE e.name = 'Cable Hip Adduction';

-- 5. Copenhagen Side Plank with Adduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Copenhagen Side Plank with Adduction',
 'In a side plank position, place your top leg on a bench or elevated surface; lift and lower your bottom leg to touch the bench by engaging inner thigh muscles while stabilizing your core.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Adductors'
WHERE e.name = 'Copenhagen Side Plank with Adduction';

-- 6. Weighted Standing Hip Adduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Weighted Standing Hip Adduction',
 'Loop a light dumbbell or weight plate between your feet, stand on one leg, and lift the weighted leg across midline by squeezing the inner thigh, then lower under control.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Adductors'
WHERE e.name = 'Weighted Standing Hip Adduction';

-- HIP ABDUCTORS
-- 1. Standing Band Hip Abduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Standing Band Hip Abduction',
 'Loop a resistance band around both legs just above the knees; stand with feet hip-width apart and push one knee outward against the band, then return under control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Abductors'
WHERE e.name = 'Standing Band Hip Abduction';

-- 2. Side-Lying Hip Abduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Side-Lying Hip Abduction',
 'Lie on your side with legs straight; lift the top leg upward toward the ceiling by engaging your outer glutes and abductors, then lower it back down slowly.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Abductors'
WHERE e.name = 'Side-Lying Hip Abduction';

-- 3. Cable Hip Abduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Cable Hip Abduction',
 'Attach an ankle cuff to a low pulley, stand perpendicular to the machine; lift the working leg out to the side against the cable resistance, squeezing abductors, then return under control.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Abductors'
WHERE e.name = 'Cable Hip Abduction';

-- 4. Banded Clamshell
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Banded Clamshell',
 'Place a loop band around your thighs just above the knees, lie on your side with hips and knees bent; open your knees against band tension by contracting outer hips, then close slowly.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Abductors'
WHERE e.name = 'Banded Clamshell';

-- 5. Seated Machine Hip Abduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Seated Machine Hip Abduction',
 'Sit in a hip-abduction machine with pads against your outer knees; push the pads outward by contracting your abductors, hold briefly, then allow them to return under control.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Abductors'
WHERE e.name = 'Seated Machine Hip Abduction';

-- 6. Weighted Standing Hip Abduction
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Weighted Standing Hip Abduction',
 'Stand with a light dumbbell or weight plate strapped to the outside of one ankle; lift that leg out to the side against gravity, keeping torso upright, then lower under control.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Hip Abductors'
WHERE e.name = 'Weighted Standing Hip Abduction';


-- FULL BODY:
-- 1. Burpee
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Burpee',
 'From standing, drop into a squat, kick feet back into a plank, perform a push-up, return feet to squat and explode up into a jump.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Burpee';

-- 2. Bear Crawl
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Bear Crawl',
 'Start on hands and knees lifted slightly off the ground; move opposite hand and foot forward simultaneously, keeping hips low and core engaged.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Bear Crawl';

-- 3. Jumping Jack
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Jumping Jack',
 'Stand with feet together and arms at sides; jump feet out wide while raising arms overhead, then jump back to start.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Jumping Jack';

-- 4. Mountain Climbers
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Mountain Climbers';

-- 5. Thruster
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Thruster',
 'Hold dumbbells at shoulders, perform a front squat, then drive upward and press the weights overhead in one fluid motion.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Thruster';

-- 6. Kettlebell Swing
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Kettlebell Swing',
 'Hinge at hips holding a kettlebell with both hands; swing it back between legs, then thrust hips forward to swing it up to shoulder height.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Kettlebell Swing';

-- 7. Renegade Row
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Renegade Row',
 'In push-up position holding dumbbells, row one weight to your hip while stabilizing your body with the opposite arm, then switch sides.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Renegade Row';

-- 8. Man Maker
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Man Maker',
 'Start in plank with dumbbells, perform a push-up, row each dumbbell, jump feet toward hands, then explode up into a dumbbell clean and press.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Man Maker';

-- 9. Turkish Get-Up
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Turkish Get-Up',
 'Lie on your back holding a kettlebell overhead; use your free hand and legs to stand up while keeping the weight locked out, then reverse back to floor.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Turkish Get-Up';

-- 10. Clean and Press
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Clean and Press',
 'Lift a barbell from floor to shoulders in a clean, then immediately press it overhead, using leg drive and arm extension.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Clean and Press';

-- 11. Sled Push
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Sled Push',
 'Load a weighted sled, lean forward with hands on the sled handles and push with powerful strides, keeping core tight and legs driving.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Sled Push';

-- 12. Deadlift High Pull
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Deadlift High Pull',
 'Perform a deadlift and, as the bar passes knees, explosively extend hips and shrug shoulders to pull the bar up to chin level.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e
JOIN muscle_groups mg ON mg.name = 'Full Body'
WHERE e.name = 'Deadlift High Pull';

-- Difficulty mapping: beginner = 1, intermediate = 2, advanced = 3
-- Muscle group lookup by name: 'full body'

-- BEGINNER (1)
-- 1. Broad Jump
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Broad Jump',
 'Stand with feet hip-width apart; bend hips and knees to load, then explode forward, landing softly in a squat.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Broad Jump';

-- 2. Lunge with Twist
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Lunge with Twist',
 'Step forward into a lunge and, at the bottom, rotate your torso over the front leg; return and repeat on the other side.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Lunge with Twist';

-- 3. High Knee March
INSERT INTO exercises (name, description, difficulty_id) VALUES
('High Knee March',
 'Stand tall and alternately lift knees toward chest in a controlled marching motion, pumping opposite arms.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'High Knee March';

-- 4. Wall Sit to Overhead Reach
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Wall Sit to Overhead Reach',
 'Lean against a wall in a chair position and hold; raise arms overhead and lower them while maintaining the wall sit.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Wall Sit to Overhead Reach';

-- 5. Star Jump
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Star Jump',
 'From a squat, explode upward into a jump, extending arms and legs into a star shape; land softly back into squat.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Star Jump';

-- 6. Sit-to-Stand
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Sit-to-Stand',
 'Start seated on a chair; stand up fully without using hands, then sit back down with control.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Sit-to-Stand';

-- 7. Plank Shoulder Tap
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Plank Shoulder Tap',
 'From high plank, lift one hand to tap the opposite shoulder, alternating while keeping hips stable.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Plank Shoulder Tap';

-- 8. Box Step-Up
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Box Step-Up',
 'Step one foot onto a low box or step, drive through the heel to lift the body up, then step back down and switch legs.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Box Step-Up';

-- 9. Standing Cross-Body Punch
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Standing Cross-Body Punch',
 'In a semi-squat, throw alternating punches across your body at midsection height, maintaining a tight core.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Standing Cross-Body Punch';

-- 10. Arm Circles Squat Combo
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Arm Circles Squat Combo',
 'Perform a squat; at the top, execute small arm circles forward for 10 reps, then backward for 10 reps, then repeat squat.',
 1);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Arm Circles Squat Combo';

-- INTERMEDIATE (2)
-- 11. Box Jump
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Box Jump',
 'Stand in front of a sturdy box; bend hips/knees and explode up onto the box, landing softly in a squat.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Box Jump';

-- 12. Medicine Ball Slam
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Medicine Ball Slam',
 'Lift a medicine ball overhead and powerfully slam it to the ground, picking it up and repeating in a fluid motion.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Medicine Ball Slam';

-- 13. Plyometric Lunge
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Plyometric Lunge',
 'From a lunge position, explosively switch legs in midair, landing in a lunge on the opposite side.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Plyometric Lunge';

-- 14. Skater Jump
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Skater Jump',
 'Jump laterally from one foot to the other, landing on a single leg and driving the opposite knee behind you.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Skater Jump';

-- 15. Kettlebell Clean and Jerk
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Kettlebell Clean and Jerk',
 'Clean a kettlebell to shoulder rack, then dip and drive it overhead, locking out, all in one fluid motion.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Kettlebell Clean and Jerk';

-- 16. Dumbbell Snatch
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Dumbbell Snatch',
 'Hinge at hips to swing a dumbbell between legs, then in one explosive movement pull it overhead and fully extend.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Dumbbell Snatch';

-- 17. Plyo Push-Up
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Plyo Push-Up',
 'Perform a push-up and explode off the floor so hands leave the ground, then land softly and repeat.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Plyo Push-Up';

-- 18. Box Jump with Knee Tuck
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Box Jump with Knee Tuck',
 'Jump onto a box, then explosively tuck knees to chest at the top before standing and stepping down.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Box Jump with Knee Tuck';

-- 19. Battle Rope Alternating Waves
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Battle Rope Alternating Waves',
 'Hold one rope in each hand and rapidly alternate waves up and down, keeping core tight and feet hip-width apart.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Battle Rope Alternating Waves';

-- 20. Burpee Box Jump
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Burpee Box Jump',
 'Perform a burpee, then immediately jump onto a box instead of straight up before stepping down and repeating.',
 2);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Burpee Box Jump';

-- ADVANCED (3)
-- 21. One-Arm Dumbbell Clean and Press
INSERT INTO exercises (name, description, difficulty_id) VALUES
('One-Arm Dumbbell Clean and Press',
 'Clean a dumbbell to shoulder rack with one arm, then dip and press it overhead, stabilizing through core and hips.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'One-Arm Dumbbell Clean and Press';

-- 22. Pistol Burpee
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Pistol Burpee',
 'Perform a burpee from a pistol (single-leg squat) position on each leg before jumping up, alternating legs each rep.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Pistol Burpee';

-- 23. Clap Pull-Up
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Clap Pull-Up',
 'Perform an explosive pull-up and at the top release the bar briefly to clap your hands before re-gripping and lowering.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Clap Pull-Up';

-- 24. Single-Arm Kettlebell Snatch
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Single-Arm Kettlebell Snatch',
 'Swing a kettlebell between legs, then in one fluid motion snatch it overhead with full lockout using one arm.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Single-Arm Kettlebell Snatch';

-- 25. Depth Jump
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Depth Jump',
 'Step off a box, land softly, then immediately explode vertically into a high jump.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Depth Jump';

-- 26. Lateral Jump Over Box
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Lateral Jump Over Box',
 'Stand beside a box, jump laterally over it landing softly on the other side, then reverse direction.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Lateral Jump Over Box';

-- 27. Handstand Push-Up
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Handstand Push-Up',
 'Kick up into a handstand against a wall and lower head toward floor by bending elbows, then press back up.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Handstand Push-Up';

-- 28. Walking Pistol Lunge
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Walking Pistol Lunge',
 'Perform a single-leg pistol squat moving forward step by step, alternating legs each rep.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Walking Pistol Lunge';

-- 29. Barbell Complex
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Barbell Complex',
 'Perform in sequence: deadlift, bent-over row, clean, front squat, press, back squat, Romanian deadlift without dropping the bar.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Barbell Complex';

-- 30. Planche Lean
INSERT INTO exercises (name, description, difficulty_id) VALUES
('Planche Lean',
 'In a push-up position, shift shoulders forward past hands, lean body weight over hands while keeping arms straight and core tight.',
 3);
INSERT INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT e.id, mg.id
FROM exercises e JOIN muscle_groups mg ON mg.name = 'full body'
WHERE e.name = 'Planche Lean';

SET @upper = (SELECT id FROM muscle_groups WHERE name='upper body');
INSERT IGNORE INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT exercise_id, @upper
  FROM exercise_muscle_groups
 WHERE muscle_group_id IN (5,6,7,8,9,14);
 
 SET @lower = (SELECT id FROM muscle_groups WHERE name='lower body');
INSERT IGNORE INTO exercise_muscle_groups (exercise_id, muscle_group_id)
SELECT exercise_id, @lower
  FROM exercise_muscle_groups
 WHERE muscle_group_id IN (10,11,12,13,15,16);
 
 

















