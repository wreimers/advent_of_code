use datafile::NumbersDataFile;
use regex::Regex;
use std::collections::{HashSet, VecDeque};
use std::fs::File;
use std::io::{BufRead, BufReader};

mod datafile;
mod day_01;

fn main() {
    main_day_06_part_02();
}

fn main_day_06_part_02() {
    let mut guard_map: Vec<Vec<char>> = Vec::new();
    let pathname = "./var/day_06_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        guard_map.push(letters);
    }
    let rows = guard_map.len();
    let cols = guard_map[0].len();
    // find guard
    let mut guard_row = 0;
    let mut guard_col = 0;
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            if guard_map[row_idx][col_idx] == '^' {
                guard_row = row_idx;
                guard_col = col_idx;
            }
        }
    }

    let guard_map_original = guard_map.clone();
    let guard_row_original = guard_row;
    let guard_col_original = guard_col;
    let mut loop_obstructions = 0;
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            guard_map = guard_map_original.clone();
            guard_row = guard_row_original;
            guard_col = guard_col_original;
            if row_idx == guard_row && col_idx == guard_col {
                continue;
            }
            if guard_map[row_idx][col_idx] == '#' {
                continue;
            }
            guard_map[row_idx][col_idx] = '#';
            println!("obstruction row:{} col:{}", row_idx, col_idx);

            let mut loop_detected = false;
            let mut next_move_off_map = false;
            let mut guard_direction = Direction::Up;
            while next_move_off_map == false {
                println!("guard_row:{} guard_col:{}", guard_row, guard_col);

                // check for off the map
                if guard_direction == Direction::Up && guard_row == 0
                    || guard_direction == Direction::Right && guard_col == cols - 1
                    || guard_direction == Direction::Down && guard_row == rows - 1
                    || guard_direction == Direction::Left && guard_col == 0
                {
                    next_move_off_map = true;
                } else {
                    if guard_direction == Direction::Up {
                        if guard_map[guard_row][guard_col] == 'U' {
                            loop_detected = true;
                        } else if guard_map[guard_row - 1][guard_col] == '#' {
                            guard_direction = Direction::Right;
                        } else {
                            guard_map[guard_row][guard_col] = 'U';
                            guard_row -= 1;
                        }
                    } else if guard_direction == Direction::Right {
                        if guard_map[guard_row][guard_col] == 'R' {
                            loop_detected = true;
                        } else if guard_map[guard_row][guard_col + 1] == '#' {
                            guard_direction = Direction::Down;
                        } else {
                            guard_map[guard_row][guard_col] = 'R';
                            guard_col += 1;
                        }
                    } else if guard_direction == Direction::Down {
                        if guard_map[guard_row][guard_col] == 'D' {
                            loop_detected = true;
                        } else if guard_map[guard_row + 1][guard_col] == '#' {
                            guard_direction = Direction::Left;
                        } else {
                            guard_map[guard_row][guard_col] = 'D';
                            guard_row += 1;
                        }
                    } else if guard_direction == Direction::Left {
                        if guard_map[guard_row][guard_col] == 'L' {
                            loop_detected = true;
                        } else if guard_map[guard_row][guard_col - 1] == '#' {
                            guard_direction = Direction::Up;
                        } else {
                            guard_map[guard_row][guard_col] = 'L';
                            guard_col -= 1;
                        }
                    }
                    if loop_detected == true {
                        loop_obstructions += 1;
                        break;
                    }
                }
            }
        }
    }
    println!("loop_obstructions:{}", loop_obstructions);
}

#[allow(dead_code)]
fn main_day_06_part_01() {
    let mut guard_map: Vec<Vec<char>> = Vec::new();
    let pathname = "./var/day_06_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        guard_map.push(letters);
    }
    let rows = guard_map.len();
    let cols = guard_map[0].len();
    // find guard
    let mut guard_row = 0;
    let mut guard_col = 0;
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            if guard_map[row_idx][col_idx] == '^' {
                guard_row = row_idx;
                guard_col = col_idx;
            }
        }
    }
    let mut next_move_off_map = false;
    let mut guard_direction = Direction::Up;
    while next_move_off_map == false {
        println!("guard_row:{} guard_col:{}", guard_row, guard_col);

        // check for off the map
        if guard_direction == Direction::Up && guard_row == 0
            || guard_direction == Direction::Right && guard_col == cols - 1
            || guard_direction == Direction::Down && guard_row == rows - 1
            || guard_direction == Direction::Left && guard_col == 0
        {
            next_move_off_map = true;
        } else {
            if guard_direction == Direction::Up {
                if guard_map[guard_row - 1][guard_col] == '#' {
                    guard_direction = Direction::Right;
                } else {
                    guard_map[guard_row][guard_col] = 'X';
                    guard_row -= 1;
                }
            } else if guard_direction == Direction::Right {
                if guard_map[guard_row][guard_col + 1] == '#' {
                    guard_direction = Direction::Down;
                } else {
                    guard_map[guard_row][guard_col] = 'X';
                    guard_col += 1;
                }
            } else if guard_direction == Direction::Down {
                if guard_map[guard_row + 1][guard_col] == '#' {
                    guard_direction = Direction::Left;
                } else {
                    guard_map[guard_row][guard_col] = 'X';
                    guard_row += 1;
                }
            } else if guard_direction == Direction::Left {
                if guard_map[guard_row][guard_col - 1] == '#' {
                    guard_direction = Direction::Up;
                } else {
                    guard_map[guard_row][guard_col] = 'X';
                    guard_col -= 1;
                }
            }
        }
    }
    let mut locations_visited = 1;
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            if guard_map[row_idx][col_idx] == 'X' {
                locations_visited += 1;
            }
        }
    }
    println!("locations_visited:{}", locations_visited);
}

#[derive(PartialEq)]
enum Direction {
    Up,
    Right,
    Down,
    Left,
}

#[allow(dead_code)]
fn main_day_05_part_02() {
    let mut rules: HashSet<String> = HashSet::new();
    let mut pages: Vec<Vec<String>> = Vec::new();
    let pathname = "./var/day_05_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        let rule_re = Regex::new(r"^\d+\|\d+$").unwrap();
        let pages_re = Regex::new(r"\d+").unwrap();
        if rule_re.is_match(&line) {
            rules.insert(line);
        } else if line == "" {
            continue;
        } else {
            let mut pages_line: Vec<String> = Vec::new();
            for mat in pages_re.find_iter(line.as_str()) {
                let page_number = &line[mat.start()..mat.end()];
                pages_line.push(page_number.to_string());
            }
            pages.push(pages_line);
        }
    }
    let mut sum = 0;
    for page_order in pages.iter_mut() {
        let mut correct_order = false;
        #[allow(unused_assignments)]
        let mut broken_rule: Option<String> = None;
        broken_rule = d05p02_check_order(&rules, &page_order);
        if broken_rule.is_none() {
            continue;
        }
        while correct_order == false {
            broken_rule = d05p02_check_order(&rules, &page_order);
            if broken_rule.is_none() {
                correct_order = true;
                continue;
            }
            let broken_rule = broken_rule.unwrap();
            let rule_re = Regex::new(r"^(\d+)\|(\d+)$").unwrap();
            let captures = rule_re.captures(&broken_rule).unwrap();
            let swap1 = captures[1].to_string();
            let swap2 = captures[2].to_string();
            let swap1_idx = page_order.iter().position(|x| *x == swap1).unwrap();
            let swap2_idx = page_order.iter().position(|x| *x == swap2).unwrap();
            page_order.swap(swap1_idx, swap2_idx);
        }
        let middle_idx = page_order.len() / 2;
        let middle_number: i32 = page_order[middle_idx].parse().unwrap();
        println!("✅ {:?} -> {}", page_order, middle_number);
        sum += middle_number;
    }
    println!("sum:{}", sum);
}

#[allow(dead_code)]
fn d05p02_check_order(rules: &HashSet<String>, page_order: &Vec<String>) -> Option<String> {
    let mut broken_rule = None;
    for candidate_idx in 0..(page_order.len() - 1) {
        let candidate = &page_order[candidate_idx];
        for check_idx in (candidate_idx + 1)..page_order.len() {
            let check = &page_order[check_idx];
            let rule = check.to_owned() + "|" + candidate.as_str();
            if rules.contains(&rule) {
                println!("❌ {} {:?}", rule, page_order);
                broken_rule = Some(rule);
                break;
            }
        }
        if broken_rule.is_some() {
            break;
        }
    }
    broken_rule
}

#[allow(dead_code)]
fn main_day_05_part_01() {
    let mut rules: HashSet<String> = HashSet::new();
    let mut pages: Vec<Vec<String>> = Vec::new();
    let pathname = "./var/day_05_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let rule_re = Regex::new(r"^\d+\|\d+$").unwrap();
        let pages_re = Regex::new(r"\d+").unwrap();
        if rule_re.is_match(&line) {
            rules.insert(line);
        } else if line == "" {
            continue;
        } else {
            let mut pages_line: Vec<String> = Vec::new();
            for mat in pages_re.find_iter(line.as_str()) {
                let page_number = &line[mat.start()..mat.end()];
                // let page_number: i32 = page_number.parse().expect("Failed to parse page_number");
                pages_line.push(page_number.to_string());
            }
            pages.push(pages_line);
        }
    }
    println!("{:?}", rules);
    println!("{:?}", pages);
    let mut sum = 0;
    for page_order in pages.iter() {
        let mut correct_order = true;
        for candidate_idx in 0..(page_order.len() - 1) {
            let candidate = &page_order[candidate_idx];
            for check_idx in (candidate_idx + 1)..page_order.len() {
                let check = &page_order[check_idx];
                let rule = check.to_owned() + "|" + candidate.as_str();
                if rules.contains(&rule) {
                    println!("❌ {} {:?}", rule, page_order);
                    correct_order = false;
                }
            }
        }
        if correct_order == true {
            let middle_idx = page_order.len() / 2;
            let middle_number: i32 = page_order[middle_idx].parse().unwrap();
            println!("✅ {:?} -> {}", page_order, middle_number);
            sum += middle_number;
        }
    }
    println!("sum:{}", sum);
}

#[allow(dead_code)]
fn main_day_04_part_02() {
    let mut file_vec: Vec<Vec<char>> = Vec::new();
    let pathname = "./var/day_04_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        file_vec.push(letters);
    }
    // println!("{:?}", file_vec);
    let rows = file_vec.len();
    let cols = file_vec[0].len();
    println!("rows:{} cols:{}", rows, cols);
    let mut x_mas_count = 0;
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            if file_vec[row_idx][col_idx] == 'A' {
                if col_idx + 1 < cols && col_idx >= 1 && row_idx + 1 < rows && row_idx >= 1 {
                    if file_vec[row_idx - 1][col_idx - 1] == 'M'
                        && file_vec[row_idx - 1][col_idx + 1] == 'M'
                        && file_vec[row_idx + 1][col_idx - 1] == 'S'
                        && file_vec[row_idx + 1][col_idx + 1] == 'S'
                    {
                        println!("✅ MAS/MAS");
                        x_mas_count += 1;
                    }
                    if file_vec[row_idx - 1][col_idx - 1] == 'S'
                        && file_vec[row_idx - 1][col_idx + 1] == 'S'
                        && file_vec[row_idx + 1][col_idx - 1] == 'M'
                        && file_vec[row_idx + 1][col_idx + 1] == 'M'
                    {
                        println!("✅ SAM/SAM");
                        x_mas_count += 1;
                    }
                    if file_vec[row_idx - 1][col_idx - 1] == 'M'
                        && file_vec[row_idx - 1][col_idx + 1] == 'S'
                        && file_vec[row_idx + 1][col_idx - 1] == 'M'
                        && file_vec[row_idx + 1][col_idx + 1] == 'S'
                    {
                        println!("✅ MAS/SAM");
                        x_mas_count += 1;
                    }
                    if file_vec[row_idx - 1][col_idx - 1] == 'S'
                        && file_vec[row_idx - 1][col_idx + 1] == 'M'
                        && file_vec[row_idx + 1][col_idx - 1] == 'S'
                        && file_vec[row_idx + 1][col_idx + 1] == 'M'
                    {
                        println!("✅ SAM/MAS");
                        x_mas_count += 1;
                    }
                }
            }
        }
    }
    println!("x_mas_count:{}", x_mas_count);
}

#[allow(dead_code)]
fn main_day_04_part_01() {
    let mut file_vec: Vec<Vec<char>> = Vec::new();
    let pathname = "./var/day_04_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        file_vec.push(letters);
    }
    // println!("{:?}", file_vec);
    let rows = file_vec.len();
    let cols = file_vec[0].len();
    println!("rows:{} cols:{}", rows, cols);
    let mut xmas_count = 0;
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            if file_vec[row_idx][col_idx] == 'X' {
                // scan to the right
                if col_idx + 3 < cols {
                    if file_vec[row_idx][col_idx + 1] == 'M'
                        && file_vec[row_idx][col_idx + 2] == 'A'
                        && file_vec[row_idx][col_idx + 3] == 'S'
                    {
                        println!("✅ right");
                        xmas_count += 1;
                    }
                }
                // scan to the left
                if col_idx >= 3 {
                    if file_vec[row_idx][col_idx - 1] == 'M'
                        && file_vec[row_idx][col_idx - 2] == 'A'
                        && file_vec[row_idx][col_idx - 3] == 'S'
                    {
                        println!("✅ left");
                        xmas_count += 1;
                    }
                }
                // scan up
                if row_idx >= 3 {
                    if file_vec[row_idx - 1][col_idx] == 'M'
                        && file_vec[row_idx - 2][col_idx] == 'A'
                        && file_vec[row_idx - 3][col_idx] == 'S'
                    {
                        println!("✅ up");
                        xmas_count += 1;
                    }
                }
                // scan down
                if row_idx + 3 < rows {
                    if file_vec[row_idx + 1][col_idx] == 'M'
                        && file_vec[row_idx + 2][col_idx] == 'A'
                        && file_vec[row_idx + 3][col_idx] == 'S'
                    {
                        println!("✅ down");
                        xmas_count += 1;
                    }
                }
                // scan diagonally up-right
                if row_idx >= 3 && col_idx + 3 < cols {
                    if file_vec[row_idx - 1][col_idx + 1] == 'M'
                        && file_vec[row_idx - 2][col_idx + 2] == 'A'
                        && file_vec[row_idx - 3][col_idx + 3] == 'S'
                    {
                        println!("✅ up-right");
                        xmas_count += 1;
                    }
                }
                // scan diagonally up-left
                if row_idx >= 3 && col_idx >= 3 {
                    if file_vec[row_idx - 1][col_idx - 1] == 'M'
                        && file_vec[row_idx - 2][col_idx - 2] == 'A'
                        && file_vec[row_idx - 3][col_idx - 3] == 'S'
                    {
                        println!("✅ up-left");
                        xmas_count += 1;
                    }
                }
                // scan diagonally down-right
                if row_idx + 3 < rows && col_idx + 3 < cols {
                    if file_vec[row_idx + 1][col_idx + 1] == 'M'
                        && file_vec[row_idx + 2][col_idx + 2] == 'A'
                        && file_vec[row_idx + 3][col_idx + 3] == 'S'
                    {
                        println!("✅ down-right");
                        xmas_count += 1;
                    }
                }
                // scan diagonally down-left
                if row_idx + 3 < rows && col_idx >= 3 {
                    if file_vec[row_idx + 1][col_idx - 1] == 'M'
                        && file_vec[row_idx + 2][col_idx - 2] == 'A'
                        && file_vec[row_idx + 3][col_idx - 3] == 'S'
                    {
                        println!("✅ down-left");
                        xmas_count += 1;
                    }
                }
            }
        }
    }
    println!("xmas_count:{}", xmas_count);
}

#[allow(dead_code)]
fn main_day_03_part_02() {
    let pathname = "./var/day_03_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    let mut sum = 0;
    let mut enabled = true;
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let re = Regex::new(r"mul\(\d+,\d+\)|do\(\)|don\'t\(\)").unwrap();
        for mat in re.find_iter(line.as_str()) {
            let multiplication = &line[mat.start()..mat.end()];
            println!("{}", multiplication);
            if multiplication == "do()" {
                enabled = true;
                continue;
            }
            if multiplication == "don't()" {
                enabled = false;
                continue;
            }
            if enabled == false {
                continue;
            }
            let mut product = 1;
            let re = Regex::new(r"\d+").unwrap();
            for mat in re.find_iter(multiplication) {
                let number = &multiplication[mat.start()..mat.end()];
                let number: i32 = number.parse().expect("Failed to parse number");
                println!("{}", number);
                product *= number;
            }
            println!("{}", product);
            sum += product;
        }
    }
    println!("{}", sum);
}

#[allow(dead_code)]
fn main_day_03_part_01() {
    let pathname = "./var/day_03_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    let mut sum = 0;
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let re = Regex::new(r"mul\(\d+,\d+\)").unwrap();
        for mat in re.find_iter(line.as_str()) {
            let multiplication = &line[mat.start()..mat.end()];
            println!("{}", multiplication);
            let mut product = 1;
            let re = Regex::new(r"\d+").unwrap();
            for mat in re.find_iter(multiplication) {
                let number = &multiplication[mat.start()..mat.end()];
                let number: i32 = number.parse().expect("Failed to parse number");
                println!("{}", number);
                product *= number;
            }
            println!("{}", product);
            sum += product;
        }
    }
    println!("{}", sum);
}

#[allow(dead_code)]
fn main_day02_part_02() {
    let pathname = "./var/day_02_sample_input.txt";
    let mut data: NumbersDataFile = NumbersDataFile::new(pathname);
    // let mut safe_reports: i32 = 0;
    while data.lines.len() > 0 {
        let mut line: VecDeque<i32> = data.lines.pop_front().unwrap();
        println!("{:?}", line);

        let mut last_number: Option<i32> = None;
        let mut safe = true;
        let mut increasing: Option<bool> = None;
        while line.len() > 0 {
            let number = line.pop_front().unwrap();
            if last_number.is_none() {
                last_number = Some(number);
                continue;
            }
            let last_number_int = last_number.unwrap();
            let difference = last_number_int - number;
            if difference == 0 || difference.abs() > 3 {
                println!("❌ {}..{} -> {}", last_number_int, number, difference.abs());
                safe = false;
            } else if increasing.is_none() && difference < 0 {
                increasing = Some(true);
            } else if increasing.is_none() && difference > 0 {
                increasing = Some(false);
            } else if increasing.is_some_and(|x| x == true) && difference > 0 {
                safe = false;
            } else if increasing.is_some_and(|x| x == false) && difference < 0 {
                safe = false;
            }
            if safe == false {
                break;
            } else {
                last_number = Some(number);
            }
        }
        if safe == true {
            // safe_reports += 1;
            println!("✅ {}", safe);
        } else {
            println!("🚫 {}", safe);
        }
    }
}

#[allow(dead_code)]
fn main_day02_part_01() {
    let filename = "./var/day_02_sample_input.txt";
    let f = File::open(filename).expect("Unable to open file");
    let f = BufReader::new(f);
    let mut safe_reports = 0;
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let re = Regex::new(r"\d+").unwrap();
        let mut safe = true;
        let mut unsafe_once = false;
        let mut increasing: Option<bool> = None;
        let mut last_number: Option<i32> = None;
        for mat in re.find_iter(line.as_str()) {
            let num_str = &line[mat.start()..mat.end()];
            let num_int: i32 = num_str.parse().expect("Failed to parse string");
            if last_number.is_none() {
                last_number = Some(num_int);
            } else {
                let last_number_int = last_number.unwrap();
                println!("{} {}", last_number_int, num_int);
                let difference = last_number_int - num_int;
                if difference.abs() > 3 {
                    safe = false;
                } else if difference == 0 {
                    safe = false;
                } else if increasing.is_none() && difference < 0 {
                    increasing = Some(true);
                } else if increasing.is_none() && difference > 0 {
                    increasing = Some(false);
                } else if increasing.is_some_and(|x| x == true) && difference > 0 {
                    safe = false;
                } else if increasing.is_some_and(|x| x == false) && difference < 0 {
                    safe = false;
                }
            }
            if safe == false && unsafe_once == false {
                unsafe_once = true;
                safe = true;
                println!("❌ {}", num_int);
            } else if safe == false {
                break;
            } else {
                last_number = Some(num_int);
            }
        }
        if safe == true {
            safe_reports += 1;
            println!("✅ {}", safe);
        } else {
            println!("🚫 {}", safe);
        }
    }
    println!("{}", safe_reports);
}

#[allow(dead_code)]
fn main_day_01_part_02() {
    let sum = day_01::part_2("./var/day_01_input.txt");
    println!("{}", sum);
}

#[allow(dead_code)]
fn main_day_01_part_01() {
    let sum = day_01::part_1("./var/day_01_input.txt");
    println!("{}", sum);
}
