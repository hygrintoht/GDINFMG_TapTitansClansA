// @ts-check
// note: changed
const express = require("express");
//const college = require("../db/models/college.js");

const chat = require("../db/models/chat.js");
const clan_leaders = require("../db/models/clan_leaders.js");
const clan_raid_events = require("../db/models/clan_raid_events.js");
const clan = require("../db/models/clan.js");
const player = require("../db/models/player.js");
const raid_score = require("../db/models/raid_score.js");

// Section: initialize express
// This is to inform the express router that the json format will be the format used 
// for the request body. Without this, req.body will cannot be parsed and will always return `undefined`!
const router = express.Router();
router.use(express.json());
router.use(express.urlencoded({ extended: true }));

router.use((req, res, next) => {
    console.log("Request received.");
    next()
});
/* template
router.get("/", (req, res) => {
    .(, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
*/
router.get("/", function (req, res) {
    res.send("<h1> Hello! </h1>");
});

//#region chat
// create
    // create chat
router.post("/chat", (req, res) => {

    const {clanID, playerID, message, messageDateTime, messageType} = req.body;
    const newChat = new chat.Chat(clanID, playerID, message, messageDateTime, messageType);

    chat.createChat(newChat, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read
    // get chat clan messages
router.get("/chat/clan_messages/:clanID", (req, res) => {

    chat.getChatClanMessages(req.params.clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// update (not needed)
// delete
    // delete chat
router.delete("/chat/:clanID", (req, res) => {

    chat.deleteChat(req.params.clanID, (err, results) =>{
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region clan
// create
    // create clan
router.post("/clan", (req, res) => {

    const name = req.body.name;
    
    clan.createClan(name, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
// read
    // get list of clans
router.get("/clan", (req, res) => {
    
    clan.getClans((err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
    // get limited list of clans
router.get("/clan/limit/:limit", (req, res) => {
    
    clan.getClansWithLimit(req.params.limit, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
    // get clan ID from clan name
router.get("/clan/get_clan_id/:clan_name", (req, res) => {

    clan.getClanID(req.params.clan_name, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});

// update
    // update clan
    // general update funtion
/* format(body) insert values needed, remove stuff if needed

{
    "clanID": 12345,
    "updateVals":
    [
        {
            "clan_name":            "",
            "num_raids_completed":  ,
            "num_clan_morale":      ,
            "required_stage":       ,
            "required_raid_level":  ,
            "privacy":              ,
        }
    ]
}

*/
router.patch("/clan", (req, res) => {

    //const {clanID, updateVals} = req.body;

    clan.updateClan(req.body.clanID, req.body.updateVals[0], (err, results) => {
        if (err) {
            console.error(err);
            res.status(500)
            res.json({message: err});
            return;
        }
        res.json(results);
    });
});

// delete
    // delete clan
router.delete("/clan/:clanID", (req, res) => {

    clan.deleteClan(req.params.clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region clan_leaders
// create
    // create clan leaders
router.post("/clan_leaders", (req, res) => {

    const {clanID, playerID, is_grandmaster} = req.body;
    const newClan_Leaders = new clan_leaders.Clan_Leaders(clanID, playerID, is_grandmaster);

    clan_leaders.createClan_Leaders(newClan_Leaders, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read
    // get clan leaders
router.get("/clan_leaders/get_clan_leaders/:clanID", (req, res) => {

    clan_leaders.getClan_LeadersInClan(req.params.clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// update (not needed)
// delete
router.delete("/clan_leaders/delete_leader/:playerID", (req, res) => {

    clan_leaders.deleteClan_Leaders(req.params.playerID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region clan_raid_events
// create
router.post("/clan_raid_events", (req, res) => {

    const {titan_name, clanID} = req.body;
    const newClan_Raid_Events = new clan_raid_events.Clan_Raid_Events(titan_name, clanID);

    clan_raid_events.createClan_Raid_Event(newClan_Raid_Events, (err,results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
// read (later)
    // get raid Id from clan Id
router.get("/clan_raid_events/get_raid_id/:clanID", (req, res) => {

    clan_raid_events.getClan_Raid_EventsID(req.params.clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// update (not needed)
// delete
router.delete("/clan_raid_events/:raidID", (req, res) => {
    
    clan_raid_events.deleteClan_Raid_Events(req.params.raidID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region player
// create
router.post("/player", (req, res) => {

    const {username, email, country, title, installation_date} = req.body;
    const newPlayer = new player.Player(username, email, country, title, installation_date);

    player.createPlayer(newPlayer, (err,results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read

router.get("/player", (req, res) => {

        player.getPlayer((err, results) => {
            if (err) {
                console.error(err);
                res.status(500);
                res.send("An error has occurred: " + err);
                return;
            }
            res.json(results);
        });
    
});

router.get("/player/stats/:playerID", (req, res) => {

    const ID = req.params.playerID;

    player.getPlayerStats(ID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
    
});

router.get("/player/ID/:username", (req, res) => {

    const name = req.params.username;

    player.getPlayerID(name, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
    
});

router.get("/clan/:clanID/players", (req, res) => {

    const clanID = req.params.clanID;

    player.getPlayersFromClan(clanID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
    
});

// update
    // general update funtion
/* format(body) insert values needed, remove stuff if needed

{
    "playerID": 12345,
    "updateVals":
    [
        {
            "username":             "",
            "email":                "",
            "clanID":               ,
            "country":              "",
            "title":                "",
            "max_prestige_stage":   ,
            "artifacts_collected":  ,
            "crafting_power":       ,
            "total_pet_levels":     ,
            "skill_points_owned":   ,
            "play_time":            ,
            "last_played":          ,
            "installation_date":    ,
        }
    ]
}

*/ 
router.patch("/player", (req, res) => {

    //const {playerID, updateVals} = req.body;

    player.updatePlayer(req.body.playerID, req.body.updateVals[0], (err, results) => {
        if (err) {
            console.error(err);
            res.status(500)
            res.json({message: err});
            return;
        }
        res.json(results);
    });
});

// delete
router.delete("/player/:playerID", (req, res) => {

    player.deletePlayer(req.params.playerID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
//#endregion

//#region raid_score
// create
router.post("/raid_score", (req, res) => {

    const {raidId, playerID, attacks, damage} = req.body;
    const newRaid_Score = new raid_score.Raid_Score(raidId, playerID, attacks, damage);

    raid_score.createRaid_Score(newRaid_Score, (err,results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });

});
// read
router.get("/raid_score/:raidID", (req, res) => {

    raid_score.getRaid_EventRaid_Score(req.params.raidID, (err, results) => {
        if (err) {
            console.error(err);
            res.status(500);
            res.send("An error has occurred: " + err);
            return;
        }
        res.json(results);
    });
});
// update (not needed)
// delete (not needed)
//#endregion

module.exports = router;