use regex::Regex;
use std::fs::File;
use std::io::{BufRead, BufReader};

fn main() {
    // let f = File::open("./var/day_01_sample_input.txt").expect("Unable to open file");
    let f = File::open("./var/day_01_part_01_input.txt").expect("Unable to open file");
    // let f = File::open("./var/day_01_mmitton_input.txt").expect("Unable to open file");
    let f = BufReader::new(f);
    let mut left_vec = Vec::new();
    let mut right_vec = Vec::new();
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
    let mut sum = 0;
    while left_vec.len() > 0 {
        let left = left_vec.pop().unwrap();
        let right = right_vec.pop().unwrap();
        println!("{} {}", left, right);
        sum += (right - left).abs();
    }

    println!("{}", sum);
}
