use regex::Regex;
use std::collections::HashMap;
use std::fs::File;
use std::io::{BufRead, BufReader};

pub struct DataFile {
    pub left: Vec<i32>,
    pub right: Vec<i32>,
}

impl DataFile {
    pub fn new(filename: &str) -> Self {
        let f = File::open(filename).expect("Unable to open file");
        let f = BufReader::new(f);
        let mut left_vec: Vec<i32> = Vec::new();
        let mut right_vec: Vec<i32> = Vec::new();
        for line in f.lines() {
            let mut num_index = 0;
            let line = line.expect("Unable to read line");
            let re = Regex::new(r"\d+").unwrap();
            for mat in re.find_iter(line.as_str()) {
                let num_str = &line[mat.start()..mat.end()];
                let num_int: i32 = num_str.parse().expect("Failed to parse string");
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

pub fn part_1(filename: &str) -> i32 {
    let mut df = DataFile::new(filename);

    let mut sum = 0;
    while df.left.len() > 0 {
        let left = df.left.pop().unwrap();
        let right = df.right.pop().unwrap();
        sum += (right - left).abs();
    }

    sum
}
