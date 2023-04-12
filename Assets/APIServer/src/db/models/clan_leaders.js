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
function getClan_Leaders(clanID, callback){
    let stmt = 'SELECT * FROM clan_leader WHERE clanID = ?'
    pool.execute(stmt, [clanID], callback); 
}
function getClan_LeadersGrandMaster(clanID, callback){
    let stmt = 'SELECT * FROM clan_leader WHERE clanID = ? AND is_grandmaster = false'
    pool.execute(stmt, [clanID], callback); 
}
// update
// delete
function deleteClan_Leaders(clanID, player, callback){
    let stmt = 'DELETE FROM Clan_Leaders WHERE clanID = ? AND playerID = ?';
    pool.execute(stmt, [clanID, playerID], callback);
}

module.exports = {
    Clan_Leaders,
    createClan_Leaders,
    getClan_Leaders,
    getClan_LeadersGrandMaster,
    deleteClan_Leaders
};