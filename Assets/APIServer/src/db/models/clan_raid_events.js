const pool = require("../db.js");

class Clan_Raid_Events{
    constructor(titan_name, clanID)
    {
        this.titan_name = titan_name;
        this.clanID = clanID;
    }
}
// create
function createClan_Raid_Event(clan_raid_events, callback){
    let stmt = 'INSERT INTO clan_raid_events (titan_name, clanID) VALUES (?,?)';
    pool.execute(stmt, [clan_raid_events.titan_name, clan_raid_events.clanID], callback);
}
// read
    // get all clan raid events
function getClan_Raid_Events(callback){
    let stmt = 'SELECT * FROM clan_raid_events';
    pool.query(stmt, [], callback);
}
    // get latest raid event id from a given clan id
function getClan_Raid_EventsID(clanID, callback){
    let stmt = 'SELECT raidID FROM clan_raid_events WHERE clanID = ? ORDER BY raidID DESC LIMIT 1';
    pool.execute(stmt, [clanID], callback);
}
// update(not needed)
// delete(not needed???)
function deleteClan_Raid_Events(raidID, callback){
    let stmt = 'DELETE FROM Clan_Raid_Events WHERE raidID = ?';
    pool.execute(stmt, [raidID], callback);
}

module.exports = {
    Clan_Raid_Events,
    createClan_Raid_Event,
    getClan_Raid_Events,
    getClan_Raid_EventsID,
    deleteClan_Raid_Events
};