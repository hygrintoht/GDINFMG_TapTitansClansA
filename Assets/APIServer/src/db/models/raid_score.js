const pool = require("../db.js");

class Raid_Score{
    constructor (raidId, playerID, attacks, damage)
    {
        this.raidId = raidId;
        this.playerID = playerID;
        this.attacks = attacks;
        this.damage = damage;
    }
}

// create
function createRaid_Score(raid_score, callback){
    let stmt = 'INSERT INTO raid_score (raidID, playerID, attacks, damage) VALUES (?,?,?,?)';
    pool.execute(stmt, [raid_score.raidId, raid_score.playerID, raid_score.attacks, raid_score.damage], callback);
}
// read
    // get list of all raid scores
function getRaid_Score(callback){
    let stmt = 'SELECT * FROM raid_score';
    pool.query(stmt, [], callback);
}
    // get list of all raid scores within a raid event from a raid id
function getRaid_EventRaid_Score(raidID, callback){
    let stmt = 'SELECT * FROM raid_score WHERE raidID = ?';
    pool.execute(stmt, [raidID], callback);
}
    // get total damage of all scores within a raid event from a raid id
function getRaid_EventTotalDamage(raidID, callback){
    let stmt = 'SELECT SUM(damage) FROM raid_score WHERE raidID = ?';
    pool.execute(stmt, [raidID], callback);
}
// update (not needed)
// delete (not needed)
module.exports = {
    Raid_Score,
    createRaid_Score,
    getRaid_Score,
    getRaid_EventTotalDamage
};