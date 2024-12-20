use datafile::NumbersDataFile;
use regex::Regex;
use std::collections::VecDeque;
use std::fs::File;
use std::io::{BufRead, BufReader};

mod datafile;
mod day_01;

fn main() {
    main_day_04_part_01();
}

fn main_day_04_part_01() {
    let mut file_vec: Vec<Vec<char>> = Vec::new();
    let pathname = "./var/day_04_sample_input.txt";
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
                    }
                }
                // scan to the left
                if col_idx >= 3 {
                    if file_vec[row_idx][col_idx - 1] == 'M'
                        && file_vec[row_idx][col_idx - 2] == 'A'
                        && file_vec[row_idx][col_idx - 3] == 'S'
                    {
                        println!("✅ left");
                    }
                }
                // scan up
                if row_idx >= 3 {
                    if file_vec[row_idx - 1][col_idx] == 'M'
                        && file_vec[row_idx - 2][col_idx] == 'A'
                        && file_vec[row_idx - 3][col_idx] == 'S'
                    {
                        println!("✅ up");
                    }
                }
                // scan down
                if row_idx + 3 < rows {
                    if file_vec[row_idx + 1][col_idx] == 'M'
                        && file_vec[row_idx + 2][col_idx] == 'A'
                        && file_vec[row_idx + 3][col_idx] == 'S'
                    {
                        println!("✅ down");
                    }
                }
                // scan diagonally up-right
                if row_idx >= 3 && col_idx + 3 < cols {
                    if file_vec[row_idx - 1][col_idx + 1] == 'M'
                        && file_vec[row_idx - 2][col_idx + 2] == 'A'
                        && file_vec[row_idx - 3][col_idx + 3] == 'S'
                    {
                        println!("✅ up-right");
                    }
                }
                // scan diagonally up-left
                if row_idx >= 3 && col_idx >= 3 {
                    if file_vec[row_idx - 1][col_idx - 1] == 'M'
                        && file_vec[row_idx - 2][col_idx - 2] == 'A'
                        && file_vec[row_idx - 3][col_idx - 3] == 'S'
                    {
                        println!("✅ up-left");
                    }
                }
            }
        }
    }
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
