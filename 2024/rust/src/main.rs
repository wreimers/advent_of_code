use regex::Regex;
use std::fs::File;
use std::io::{BufRead, BufReader};

mod day_01;

fn main() {
    main_day02_part_01();
}

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
            } else if safe == false && unsafe_once == true {
                break;
            } else {
                last_number = Some(num_int);
            }
        }
        if safe == true {
            safe_reports += 1;
        }
        println!("{}", safe);
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
