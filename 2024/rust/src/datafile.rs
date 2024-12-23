use regex::Regex;
use std::collections::VecDeque;
use std::fs::File;
use std::io::{BufRead, BufReader};

pub struct NumbersDataFile {
    pub lines: VecDeque<VecDeque<i32>>,
}

impl NumbersDataFile {
    pub fn new(pathname: &str) -> Self {
        Self {
            lines: NumbersDataFile::read_lines(pathname),
        }
    }

    fn read_lines(pathname: &str) -> VecDeque<VecDeque<i32>> {
        let mut result: VecDeque<VecDeque<i32>> = VecDeque::new();
        let f = File::open(pathname).expect("Unable to open file");
        let f = BufReader::new(f);
        for line in f.lines() {
            let line = line.expect("Unable to read line");
            let parsed_line: VecDeque<i32> = NumbersDataFile::parse_line(line);
            result.push_back(parsed_line);
        }
        result
    }

    fn parse_line(line: String) -> VecDeque<i32> {
        let mut result: VecDeque<i32> = VecDeque::new();
        let re = Regex::new(r"\d+").unwrap();
        for mat in re.find_iter(line.as_str()) {
            let number = &line[mat.start()..mat.end()];
            let number: i32 = number.parse().expect("Failed to parse string");
            result.push_back(number);
        }
        result
    }
}
