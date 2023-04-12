CREATE SCHEMA IF NOT EXISTS taptitans_database;

USE taptitans_database;

DROP TABLE IF EXISTS chat;
DROP TABLE IF EXISTS raid_score;
DROP TABLE IF EXISTS clan_raid_events;
DROP TABLE IF EXISTS clan_leaders;
DROP TABLE IF EXISTS player;
DROP TABLE IF EXISTS clan;

CREATE TABLE IF NOT EXISTS clan (
	clanID int NOT NULL AUTO_INCREMENT,
    clan_invite_code int NOT NULL DEFAULT (rand() * 10000),
    clan_name VARCHAR(60) NOT NULL,
    num_raids_completed int NOT NULL DEFAULT 0,
    num_clan_morale int NOT NULL DEFAULT 0,
    required_stage int NOT NULL DEFAULT 0,
    required_raid_level int NOT NULL DEFAULT 0,
    privacy ENUM('unknown', 'public', 'private') NOT NULL DEFAULT 'public',
	CONSTRAINT `PK_clanID_key` PRIMARY KEY (clanID)
);

CREATE TABLE IF NOT EXISTS player (
	playerID int NOT NULL AUTO_INCREMENT,
    username CHAR(60) NOT NULL UNIQUE,
    email VARCHAR(60),
    clanID int,
    country CHAR(3) NOT NULL,
    title ENUM('unknown', 'Titan Slayer', 'Master', 'Champion', 'Hero'),
    max_prestige_stage int NOT NULL DEFAULT 0,
    artifacts_collected int NOT NULL DEFAULT 0,
    crafting_power int NOT NULL DEFAULT 1,
    total_pet_levels int NOT NULL DEFAULT 0,
    skill_points_owned  int NOT NULL DEFAULT 0,
    play_time TIME,
    last_played DATETIME,
    installation_date DATE NOT NULL,
    CONSTRAINT `PK_playerID_key` PRIMARY KEY (playerID),
    CONSTRAINT `FK_clanID_in_player_key` FOREIGN KEY (clanID)
		REFERENCES clan(clanID)
        ON UPDATE CASCADE
        ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS clan_leaders (
	clanID int NOT NULL,
    playerID int NOT NULL,
    is_grandmaster bool NOT NULL DEFAULT true,
    CONSTRAINT `PK_clan_leader_key` PRIMARY KEY (clanID, playerID),
    CONSTRAINT `FK_clanID_in_leader_key` FOREIGN KEY (clanID)
		REFERENCES clan(clanID),
	CONSTRAINT `FK_playerID_in_leader_key` FOREIGN KEY (playerID)
		REFERENCES player(playerID)
);

CREATE TABLE IF NOT EXISTS clan_raid_events (
	raidID int NOT NULL AUTO_INCREMENT,
    titan_name VARCHAR(30) NOT NULL,
    clanID int NOT NULL,
    CONSTRAINT `PK_raidID_key` PRIMARY KEY (raidID),
    CONSTRAINT `FK_clanID_in_raid_events_key` FOREIGN KEY (clanID)
		REFERENCES clan(clanID)
);

CREATE TABLE IF NOT EXISTS raid_score(
	raidID int NOT NULL,
    playerID int NOT NULL,
    attacks int NOT NULL DEFAULT 0,
    damage int NOT NULL DEFAULT 0,
    CONSTRAINT `PK_raid_score_key` PRIMARY KEY (playerID, raidID),
    CONSTRAINT `FK_playerID_in_score_key` FOREIGN KEY (playerID)
		REFERENCES player(playerID),
    CONSTRAINT `FK_raidID_in_score_key` FOREIGN KEY (raidID)
		REFERENCES clan_raid_events(raidID)
);

CREATE TABLE IF NOT EXISTS chat (
	chatID int NOT NULL AUTO_INCREMENT,
    clanID int NOT NULL,
    playerID int NOT NULL,
    message VARCHAR(200) NOT NULL,
    messageDateTime DATETIME NOT NULL,
    messageType ENUM('unknown', 'player_chat', 'raid_closed', 'raid_initiated', 'raid_active', 'clan_crate'),
    CONSTRAINT `PK_chatID_key` PRIMARY KEY (chatID),
    CONSTRAINT `FK_clanID_in_chat_key` FOREIGN KEY (clanID)
		REFERENCES clan(clanID),
	CONSTRAINT `FK_playerID_in_chat_key` FOREIGN KEY (playerID)
		REFERENCES player(playerID)
);