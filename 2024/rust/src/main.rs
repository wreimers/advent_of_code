use std::fs::File;
use std::io::{BufRead, BufReader};
use regex::Regex;

fn main() {
    let f = File::open("./var/day_01_sample_input.txt").expect("Unable to open file");
    // let f = File::open("./var/day_01_part_01_input.txt").expect("Unable to open file");
    let f = BufReader::new(f);
    let mut left_sum: i64 = 0;
    let mut right_sum: i64 = 0;
    for line in f.lines() {
        let mut num_index = 0;
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let re = Regex::new(r"\d+").unwrap();
        for mat in re.find_iter(line.as_str()) {
            let num_str = &line[mat.start()..mat.end()];
            let num_int: i64 = num_str.parse().expect("Failed to parse string");
            if num_index == 0 {
                left_sum += num_int;
            }
            else {
                right_sum += num_int;
            }
            num_index += 1;
        }
        println!(".. {}", right_sum - left_sum);
    }
}
