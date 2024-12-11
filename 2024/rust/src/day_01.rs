use regex::Regex;
use std::fs::File;
use std::io::{BufRead, BufReader};

pub struct DataFile {
    pub left: Vec<i64>,
    pub right: Vec<i64>,
}

impl DataFile {
    pub fn new(filename: &str) -> Self {
        let f = File::open(filename).expect("Unable to open file");
        let f = BufReader::new(f);
        let mut left_vec: Vec<i64> = Vec::new();
        let mut right_vec: Vec<i64> = Vec::new();
        for line in f.lines() {
            let mut num_index = 0;
            let line = line.expect("Unable to read line");
            let re = Regex::new(r"\d+").unwrap();
            for mat in re.find_iter(line.as_str()) {
                let num_str = &line[mat.start()..mat.end()];
                let num_int: i64 = num_str.parse().expect("Failed to parse string");
                if num_index == 0 {
                    left_vec.push(num_int);
                } else {
                    right_vec.push(num_int);
                }
                num_index += 1;
            }
        }
        left_vec.sort();
        right_vec.sort();
        Self {
            left: left_vec,
            right: right_vec,
        }
    }
}
