use datafile::NumbersDataFile;
use itertools::Itertools;
use memoize::memoize;
use regex::Regex;
use std::collections::{HashMap, HashSet, VecDeque};
use std::fs::File;
use std::io::{BufRead, BufReader};

mod datafile;
mod day_01;

fn main() {
    let start = std::time::Instant::now();
    main_day_10_part_02();
    println!("{:?}", start.elapsed());
}

fn main_day_10_part_02() {
    let mut trail_map: Vec<Vec<char>> = Vec::new();
    let pathname = "./var/day_10_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        trail_map.push(letters);
    }
    // dbg!(&trail_map);

    let rows = trail_map.len();
    let cols = trail_map[0].len();

    let mut trailheads: Vec<TrailPoint> = Vec::new();
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            let point_char = trail_map[row_idx][col_idx];
            if point_char == '0' {
                trailheads.push(TrailPoint {
                    point_char: point_char,
                    row_idx: row_idx,
                    col_idx: col_idx,
                });
            }
        }
    }
    // dbg!(&trailheads);

    let mut paths: Vec<TrailPoint> = Vec::new();
    let mut total_rating = 0;
    for i in 0..trailheads.len() {
        let mut scores: HashSet<TrailPoint> = HashSet::new();
        let trail_point = trailheads[i];
        paths.push(trail_point);
        loop {
            let curr_point = paths.pop().unwrap();
            let mut result = d10p01_look_around(&trail_map, &curr_point);
            // dbg!(&result);
            paths.append(&mut result);
            for j in 0..paths.len() {
                let j_point = paths[j];
                if j_point.point_char == '9' {
                    scores.insert(j_point);
                }
            }
            if paths.is_empty() {
                break;
            }
        }
        // dbg!(&scores);
        total_rating += scores.len();
    }
    dbg!(&total_rating);
}

#[allow(dead_code)]
fn main_day_10_part_01() {
    let mut trail_map: Vec<Vec<char>> = Vec::new();
    let pathname = "./var/day_10_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        trail_map.push(letters);
    }
    // dbg!(&trail_map);

    let rows = trail_map.len();
    let cols = trail_map[0].len();

    let mut trailheads: Vec<TrailPoint> = Vec::new();
    // let mut trailends: Vec<TrailPoint> = Vec::new();
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            let point_char = trail_map[row_idx][col_idx];
            if point_char == '0' {
                trailheads.push(TrailPoint {
                    point_char: point_char,
                    row_idx: row_idx,
                    col_idx: col_idx,
                });
            }
            // else if point_char == '9' {
            //     trailends.push(TrailPoint {
            //         point_char: point_char,
            //         row_idx: row_idx,
            //         col_idx: col_idx,
            //     });
            // }
        }
    }
    // dbg!(&trailheads);
    // dbg!(&trailends);

    let mut paths: Vec<TrailPoint> = Vec::new();
    let mut total_score = 0;
    for i in 0..trailheads.len() {
        let mut scores: HashSet<TrailPoint> = HashSet::new();
        let trail_point = trailheads[i];
        paths.push(trail_point);
        loop {
            let curr_point = paths.pop().unwrap();
            let mut result = d10p01_look_around(&trail_map, &curr_point);
            // dbg!(&result);
            paths.append(&mut result);
            for j in 0..paths.len() {
                let j_point = paths[j];
                if j_point.point_char == '9' {
                    scores.insert(j_point);
                }
            }
            if paths.is_empty() {
                break;
            }
        }
        // dbg!(&scores);
        total_score += scores.len();
    }
    dbg!(&total_score);
}

#[derive(Debug, Clone, PartialEq, Eq, Hash, Copy)]
struct TrailPoint {
    point_char: char,
    row_idx: usize,
    col_idx: usize,
}

fn d10p01_look_around(trail_map: &Vec<Vec<char>>, trail_point: &TrailPoint) -> Vec<TrailPoint> {
    let mut result: Vec<TrailPoint> = Vec::new();
    let rows = trail_map.len();
    let cols = trail_map[0].len();
    let center = trail_map[trail_point.row_idx][trail_point.col_idx];
    let mut target = ' ';
    if center == '0' {
        target = '1';
    } else if center == '1' {
        target = '2';
    } else if center == '2' {
        target = '3';
    } else if center == '3' {
        target = '4';
    } else if center == '4' {
        target = '5';
    } else if center == '5' {
        target = '6';
    } else if center == '6' {
        target = '7';
    } else if center == '7' {
        target = '8';
    } else if center == '8' {
        target = '9';
    }
    if trail_point.row_idx >= 1 {
        // look up
        if trail_map[trail_point.row_idx - 1][trail_point.col_idx] == target {
            result.push(TrailPoint {
                point_char: target,
                row_idx: trail_point.row_idx - 1,
                col_idx: trail_point.col_idx,
            });
        }
    }
    if trail_point.row_idx < rows - 1 {
        // look down
        if trail_map[trail_point.row_idx + 1][trail_point.col_idx] == target {
            result.push(TrailPoint {
                point_char: target,
                row_idx: trail_point.row_idx + 1,
                col_idx: trail_point.col_idx,
            });
        }
    }
    if trail_point.col_idx >= 1 {
        // look left
        if trail_map[trail_point.row_idx][trail_point.col_idx - 1] == target {
            result.push(TrailPoint {
                point_char: target,
                row_idx: trail_point.row_idx,
                col_idx: trail_point.col_idx - 1,
            });
        }
    }
    if trail_point.col_idx < cols - 1 {
        // look right
        if trail_map[trail_point.row_idx][trail_point.col_idx + 1] == target {
            result.push(TrailPoint {
                point_char: target,
                row_idx: trail_point.row_idx,
                col_idx: trail_point.col_idx + 1,
            });
        }
    }
    result
}

#[allow(dead_code)]
fn main_day_09_part_02() {
    let mut letters: Vec<char> = Vec::new();
    let pathname = "./var/day_09_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        letters = line.chars().collect();
        // println!("{:?}", letters);
    }

    let mut disk_files: VecDeque<DiskFile> = VecDeque::new();
    let mut disk_spaces: VecDeque<DiskFile> = VecDeque::new();
    let mut disk_blocks: VecDeque<DiskBlock> = VecDeque::new();
    let mut current_idx: i32 = 0;
    for idx in 0..letters.len() {
        let block_count: i32 = letters[idx].to_string().parse().unwrap();
        // println!("block_count:{} idx:{}", block_count, idx);
        if idx % 2 == 0 {
            // even - file blocks
            // println!("even idx:{}", idx);
            disk_files.push_back(DiskFile {
                index: current_idx as i32,
                is_space: false,
                size: block_count,
                file_id: Some((idx / 2) as i32),
            });
            for _i in 0..block_count {
                let db = DiskBlock {
                    is_space: false,
                    file_id: Some((idx / 2) as i32),
                };
                // println!("db:{:?}", db);
                disk_blocks.push_back(db);
            }
        } else {
            // odd - space blocks
            // println!("odd idx:{}", idx);
            disk_spaces.push_back(DiskFile {
                index: current_idx as i32,
                is_space: true,
                size: block_count,
                file_id: None,
            });
            for _i in 0..block_count {
                let db = DiskBlock {
                    is_space: true,
                    file_id: None,
                };
                // println!("db:{:?}", db);
                disk_blocks.push_back(db);
            }
        }
        current_idx += block_count;
    }
    // dbg!(&disk_blocks);
    // dbg!(&disk_files);
    // dbg!(&disk_spaces);

    for i in (1..disk_files.len()).rev() {
        let disk_file = disk_files[i];
        // println!("disk_file:{:?}", &disk_file);
        if disk_file.is_space == false {
            for j in 0..disk_spaces.len() {
                let disk_space = disk_spaces[j];
                if disk_space.index >= disk_file.index {
                    break;
                }
                if disk_space.size >= disk_file.size {
                    // println!("disk_space:{:?}", &disk_space);
                    // move the file to the space
                    for k in 0..disk_file.size {
                        // println!(
                        //     "moving block {} to block {}",
                        //     disk_file.index + k,
                        //     disk_space.index + k,
                        // );
                        disk_blocks[(disk_space.index + k) as usize] =
                            disk_blocks[(disk_file.index + k) as usize];
                        disk_blocks[(disk_file.index + k) as usize] = DiskBlock {
                            is_space: true,
                            file_id: None,
                        }
                    }
                    // recalculate disk_spaces
                    disk_spaces[j] = DiskFile {
                        index: disk_spaces[j].index + disk_file.size,
                        is_space: true,
                        size: disk_space.size - disk_file.size,
                        file_id: None,
                    };
                    // println!("new disk_space:{:?}", &disk_spaces[j]);
                    break;
                }
            }
        }
    }
    // dbg!(&disk_blocks);

    let mut checksum: i64 = 0;
    for idx in 0..disk_blocks.len() {
        if disk_blocks[idx].is_space == false {
            checksum += idx as i64 * disk_blocks[idx].file_id.unwrap() as i64;
        }
    }
    dbg!(&checksum);
}

#[derive(Debug, Clone, PartialEq, Eq, Hash, Copy)]
struct DiskFile {
    index: i32,
    is_space: bool,
    size: i32,
    file_id: Option<i32>,
}

#[allow(dead_code)]
fn main_day_09_part_01() {
    let mut letters: Vec<char> = Vec::new();
    let pathname = "./var/day_09_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        letters = line.chars().collect();
        println!("{:?}", letters);
    }
    let mut disk_blocks: VecDeque<DiskBlock> = VecDeque::new();
    for idx in 0..letters.len() {
        let block_count: i32 = letters[idx].to_string().parse().unwrap();
        println!("block_count:{} idx:{}", block_count, idx);
        if idx % 2 == 0 {
            // even - file blocks
            // println!("even idx:{}", idx);
            for _i in 0..block_count {
                let db = DiskBlock {
                    is_space: false,
                    file_id: Some((idx / 2) as i32),
                };
                println!("db:{:?}", db);
                disk_blocks.push_back(db);
            }
        } else {
            // odd - space blocks
            // println!("odd idx:{}", idx);
            for _i in 0..block_count {
                let db = DiskBlock {
                    is_space: true,
                    file_id: None,
                };
                println!("db:{:?}", db);
                disk_blocks.push_back(db);
            }
        }
    }
    dbg!(&disk_blocks);
    let mut file_block_indices: Vec<usize> = Vec::new();
    for idx in 0..disk_blocks.len() {
        if disk_blocks[idx].is_space == false {
            file_block_indices.push(idx);
        }
    }
    dbg!(&file_block_indices);
    for idx in 0..disk_blocks.len() {
        if disk_blocks[idx].is_space == false {
            continue;
        } else {
            if file_block_indices.len() == 0 {
                break;
            }
            println!("Found space at idx:{}", &idx);
            let file_block_idx = file_block_indices.pop().unwrap();
            if idx > file_block_idx {
                println!("hit boundary");
                break;
            }
            println!("Swapping with file_block_idx:{}", &file_block_idx);
            disk_blocks[idx] = disk_blocks[file_block_idx];
            disk_blocks[file_block_idx] = DiskBlock {
                is_space: true,
                file_id: None,
            };
        }
    }
    dbg!(&disk_blocks);
    let mut checksum: i64 = 0;
    for idx in 0..disk_blocks.len() {
        if disk_blocks[idx].is_space == false {
            checksum += idx as i64 * disk_blocks[idx].file_id.unwrap() as i64;
        }
    }
    dbg!(&checksum);
}

#[derive(Debug, Clone, PartialEq, Eq, Hash, Copy)]
struct DiskBlock {
    is_space: bool,
    file_id: Option<i32>,
}

#[allow(dead_code)]
fn main_day_08_part_02() {
    let mut node_map: Vec<Vec<char>> = Vec::new();
    let mut nodes: HashMap<char, Vec<AntennaNode>> = HashMap::new();
    let pathname = "./var/day_08_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        node_map.push(letters);
    }
    let rows = node_map.len();
    let cols = node_map[0].len();
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            let node_type = node_map[row_idx][col_idx];
            if node_type == '.' {
                continue;
            }
            let node = AntennaNode {
                node_type: node_type,
                row: row_idx,
                col: col_idx,
            };
            if !nodes.contains_key(&node_type) {
                nodes.insert(node_type, Vec::new());
            }
            nodes.get_mut(&node_type).unwrap().push(node);
        }
    }
    // println!("{:?}", nodes);
    let mut anodes: HashSet<AntennaAntinode> = HashSet::new();
    for (_node_type, node_list) in nodes.into_iter() {
        let combinations: HashSet<_> = node_list.into_iter().combinations(2).collect();
        // println!("combinations:{:#?}", combinations);

        for comb in combinations {
            let row_distance = (comb[0].row as i32 - comb[1].row as i32).abs();
            let col_distance = (comb[0].col as i32 - comb[1].col as i32).abs();
            // Add the nodes as anodes
            let anode = AntennaAntinode {
                row: comb[0].row as i32,
                col: comb[0].col as i32,
            };
            anodes.insert(anode);
            let anode = AntennaAntinode {
                row: comb[1].row as i32,
                col: comb[1].col as i32,
            };
            anodes.insert(anode);
            if comb[0].row <= comb[1].row && comb[0].col <= comb[1].col {
                // comb[0] is up and left
                let mut current_row_distance = 0;
                let mut current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[0].row as i32 - current_row_distance,
                        col: comb[0].col as i32 - current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
                current_row_distance = 0;
                current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[1].row as i32 + current_row_distance,
                        col: comb[1].col as i32 + current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
            } else if comb[0].row >= comb[1].row && comb[0].col <= comb[1].col {
                // comb[0] is down and left
                let mut current_row_distance = 0;
                let mut current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[0].row as i32 + current_row_distance,
                        col: comb[0].col as i32 - current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
                current_row_distance = 0;
                current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[1].row as i32 - current_row_distance,
                        col: comb[1].col as i32 + current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
            } else if comb[0].row <= comb[1].row && comb[0].col >= comb[1].col {
                // comb[0] is up and right
                let mut current_row_distance = 0;
                let mut current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[0].row as i32 - current_row_distance,
                        col: comb[0].col as i32 + current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
                current_row_distance = 0;
                current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[1].row as i32 + current_row_distance,
                        col: comb[1].col as i32 - current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
            } else if comb[0].row >= comb[1].row && comb[0].col >= comb[1].col {
                // comb[0] is down and right
                let mut current_row_distance = 0;
                let mut current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[0].row as i32 + current_row_distance,
                        col: comb[0].col as i32 + current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
                current_row_distance = 0;
                current_col_distance = 0;
                loop {
                    current_row_distance += row_distance;
                    current_col_distance += col_distance;
                    let anode = AntennaAntinode {
                        row: comb[1].row as i32 - current_row_distance,
                        col: comb[1].col as i32 - current_col_distance,
                    };
                    if d08p02_node_in_bounds(&anode, &rows, &cols) {
                        anodes.insert(anode);
                    } else {
                        break;
                    }
                }
            }
        }
    }
    // cull out of bounds anodes
    let mut in_bounds_anodes: Vec<AntennaAntinode> = Vec::new();
    for anode in anodes {
        if d08p02_node_in_bounds(&anode, &rows, &cols) {
            in_bounds_anodes.push(anode);
        }
    }
    // println!("anodes:{:#?}", anodes);
    // println!("anode_count:{}", anodes.len());
    println!("in_bounds_anodes:{}", in_bounds_anodes.len());
}

fn d08p02_node_in_bounds(anode: &AntennaAntinode, rows: &usize, cols: &usize) -> bool {
    let mut in_bounds = false;
    if anode.row >= 0 && anode.row < *rows as i32 && anode.col >= 0 && anode.col < *cols as i32 {
        in_bounds = true;
    }
    in_bounds
}

#[allow(dead_code)]
fn main_day_08_part_01() {
    let mut node_map: Vec<Vec<char>> = Vec::new();
    let mut nodes: HashMap<char, Vec<AntennaNode>> = HashMap::new();
    let pathname = "./var/day_08_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        // println!("{}", line);
        let letters: Vec<char> = line.chars().collect();
        // println!("{:?}", letters);
        node_map.push(letters);
    }
    let rows = node_map.len();
    let cols = node_map[0].len();
    for row_idx in 0..rows {
        for col_idx in 0..cols {
            let node_type = node_map[row_idx][col_idx];
            if node_type == '.' {
                continue;
            }
            let node = AntennaNode {
                node_type: node_type,
                row: row_idx,
                col: col_idx,
            };
            if !nodes.contains_key(&node_type) {
                nodes.insert(node_type, Vec::new());
            }
            nodes.get_mut(&node_type).unwrap().push(node);
        }
    }
    // println!("{:?}", nodes);
    let mut anodes: HashSet<AntennaAntinode> = HashSet::new();
    for (_node_type, node_list) in nodes.into_iter() {
        let combinations: HashSet<_> = node_list.into_iter().combinations(2).collect();
        // println!("combinations:{:#?}", combinations);

        for comb in combinations {
            let row_distance = (comb[0].row as i32 - comb[1].row as i32).abs();
            let col_distance = (comb[0].col as i32 - comb[1].col as i32).abs();
            if comb[0].row <= comb[1].row && comb[0].col <= comb[1].col {
                // comb[0] is up and left
                let anode = AntennaAntinode {
                    // node_type: node_type,
                    row: comb[0].row as i32 - row_distance,
                    col: comb[0].col as i32 - col_distance,
                };
                anodes.insert(anode);
                let anode = AntennaAntinode {
                    row: comb[1].row as i32 + row_distance,
                    col: comb[1].col as i32 + col_distance,
                };
                anodes.insert(anode);
            } else if comb[0].row >= comb[1].row && comb[0].col <= comb[1].col {
                // comb[0] is down and left
                let anode = AntennaAntinode {
                    row: comb[0].row as i32 + row_distance,
                    col: comb[0].col as i32 - col_distance,
                };
                anodes.insert(anode);
                let anode = AntennaAntinode {
                    row: comb[1].row as i32 - row_distance,
                    col: comb[1].col as i32 + col_distance,
                };
                anodes.insert(anode);
            } else if comb[0].row <= comb[1].row && comb[0].col >= comb[1].col {
                // comb[0] is up and right
                let anode = AntennaAntinode {
                    row: comb[0].row as i32 - row_distance,
                    col: comb[0].col as i32 + col_distance,
                };
                anodes.insert(anode);
                let anode = AntennaAntinode {
                    row: comb[1].row as i32 + row_distance,
                    col: comb[1].col as i32 - col_distance,
                };
                anodes.insert(anode);
            } else if comb[0].row >= comb[1].row && comb[0].col >= comb[1].col {
                // comb[0] is down and right
                let anode = AntennaAntinode {
                    row: comb[0].row as i32 + row_distance,
                    col: comb[0].col as i32 + col_distance,
                };
                anodes.insert(anode);
                let anode = AntennaAntinode {
                    row: comb[1].row as i32 - row_distance,
                    col: comb[1].col as i32 - col_distance,
                };
                anodes.insert(anode);
            }
        }
    }
    // todo cull out of bounds anodes
    let mut in_bounds_anodes: Vec<AntennaAntinode> = Vec::new();
    for anode in anodes {
        if anode.row >= 0 && anode.row < rows as i32 && anode.col >= 0 && anode.col < cols as i32 {
            in_bounds_anodes.push(anode);
        }
    }
    // println!("anodes:{:#?}", anodes);
    // println!("anode_count:{}", anodes.len());
    println!("in_bounds_anodes:{}", in_bounds_anodes.len());
}

#[derive(Debug, Clone, PartialEq, Eq, Hash)]
struct AntennaNode {
    node_type: char,
    row: usize,
    col: usize,
}

#[derive(Debug, Clone, PartialEq, Eq, Hash)]
struct AntennaAntinode {
    // node_type: char,
    row: i32,
    col: i32,
}

#[allow(dead_code)]
fn main_day_07_part_02() {
    let pathname = "./var/day_07_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    let mut possible_equations = 0;
    let mut sum = 0;
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let re = Regex::new(r"\d+").unwrap();
        let captures: Vec<&str> = re.find_iter(line.as_str()).map(|x| x.as_str()).collect();

        let mut operators: Vec<char> = Vec::new();
        for _ in 1..(captures.len() - 1) {
            operators.push('+');
            operators.push('*');
            operators.push('|');
        }
        println!("operators:{:?}", operators);
        let k = captures.len() - 2;
        let combinations = d07p02_calculate_combinations(operators, k);
        // println!("combinations:{:?}", combinations);
        let target_value: i64 = captures[0].parse().unwrap();
        for comb in combinations {
            println!("combination:{:?}", comb);
            let mut value = 0;
            for idx in 1..(captures.len()) {
                let operand = captures[idx].parse().unwrap();
                if value == 0 {
                    value = operand;
                } else {
                    let operator = comb[idx - 2];
                    if operator == '+' {
                        value += operand;
                    } else if operator == '*' {
                        value *= operand;
                    } else if operator == '|' {
                        value = (value.to_string() + &operand.to_string()).parse().unwrap();
                    }
                }
                println!("target_value:{} value:{}", target_value, value);
            }
            if value == target_value {
                println!("found target value with combination:{:?}", comb);
                possible_equations += 1;
                sum += target_value;
                break;
            }
        }
    }
    println!("possible_equations:{} sum:{}", possible_equations, sum);
}

#[memoize]
fn d07p02_calculate_combinations(operators: Vec<char>, k: usize) -> HashSet<Vec<char>> {
    let combinations: HashSet<_> = operators.into_iter().combinations(k).collect();
    combinations
}

#[allow(dead_code)]
fn main_day_07_part_01() {
    let pathname = "./var/day_07_input.txt";
    let f = File::open(pathname).expect("Unable to open file");
    let f = BufReader::new(f);
    let mut possible_equations = 0;
    let mut sum = 0;
    for line in f.lines() {
        let line = line.expect("Unable to read line");
        println!("{}", line);
        let re = Regex::new(r"\d+").unwrap();
        let captures: Vec<&str> = re.find_iter(line.as_str()).map(|x| x.as_str()).collect();

        let mut operands: Vec<char> = Vec::new();
        for _ in 1..(captures.len() - 1) {
            operands.push('+');
            operands.push('*');
        }
        println!("operands:{:?}", operands);
        // let operands = vec!['+', '*', '+', '*'];
        let combinations: HashSet<_> = operands
            .into_iter()
            .combinations(captures.len() - 2)
            .collect();
        println!("combinations:{:?}", combinations);
        let target_value: i64 = captures[0].parse().unwrap();
        for comb in combinations {
            println!("combination:{:?}", comb);
            let mut value = 0;
            for idx in 1..(captures.len()) {
                let operand = captures[idx].parse().unwrap();
                if value == 0 {
                    value = operand;
                } else {
                    let operator = comb[idx - 2];
                    if operator == '+' {
                        value += operand;
                    } else if operator == '*' {
                        value *= operand;
                    }
                }
                println!("target_value:{} value:{}", target_value, value);
            }
            if value == target_value {
                println!("found target value with combination:{:?}", comb);
                possible_equations += 1;
                sum += target_value;
                break;
            }
        }
    }
    println!("possible_equations:{} sum:{}", possible_equations, sum);
}

#[allow(dead_code)]
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
