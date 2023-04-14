const pool = require("../db.js");

class Clan_Leaders{
    constructor(clanID, playerID, is_grandmaster){
        this.clanID = clanID;
        this.playerID = playerID;
        this.is_grandmaster = is_grandmaster;
    }
}

// create
function createClan_Leaders(clan_leaders, callback)
{
    let stmt = 'INSERT INTO clan_leaders (clanID, playerID, is_grandmaster) VALUES (?,?,?)';
    pool.execute(stmt, [clan_leaders.clanID, clan_leaders.playerID, clan_leaders.is_grandmaster], callback); 
}
// read
    // gets clan leaders based on clan id
function getClan_LeadersInClan(clanID, callback){
    //let stmt = 'SELECT * FROM clan_leader WHERE clanID = ?';
    let stmt = 'SELECT player.username FROM clan_leader INNER JOIN player ON player.playerID = clan_leader.playerID WHERE clan_leader.clanID = ?';
    pool.execute(stmt, [clanID], callback); 
}
// update
// delete
function deleteClan_Leaders(playerID, callback){
    let stmt = 'DELETE FROM Clan_Leaders WHERE playerID = ?';
    pool.execute(stmt, [playerID], callback);
}

module.exports = {
    Clan_Leaders,
    createClan_Leaders,
    getClan_LeadersInClan,
    deleteClan_Leaders
};