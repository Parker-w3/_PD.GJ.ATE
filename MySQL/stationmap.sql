/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50162
Source Host           : localhost:3306
Source Database       : autoflow

Target Server Type    : MYSQL
Target Server Version : 50162
File Encoding         : 65001

Date: 2017-12-03 09:57:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `stationmap`
-- ----------------------------
DROP TABLE IF EXISTS `stationmap`;
CREATE TABLE `stationmap` (
  `idNo` int(11) NOT NULL DEFAULT '0',
  `StationId` int(11) DEFAULT '0',
  `StationName` varchar(10) DEFAULT NULL,
  `StationForbit` int(10) DEFAULT '0',
  PRIMARY KEY (`idNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of stationmap
-- ----------------------------
INSERT INTO `stationmap` VALUES ('0', '1', 'LOADUP', '0');
INSERT INTO `stationmap` VALUES ('1', '2', 'BURNIN', '0');
INSERT INTO `stationmap` VALUES ('2', '3', 'TURNON', '0');
INSERT INTO `stationmap` VALUES ('3', '4', 'HIPOT1', '0');
INSERT INTO `stationmap` VALUES ('4', '5', 'HIPOT2', '0');
INSERT INTO `stationmap` VALUES ('5', '6', 'ATE1', '0');
INSERT INTO `stationmap` VALUES ('6', '7', 'ATE2', '0');
INSERT INTO `stationmap` VALUES ('7', '8', 'ATE3', '0');
INSERT INTO `stationmap` VALUES ('8', '9', 'ATE4', '0');
INSERT INTO `stationmap` VALUES ('9', '10', 'RRCC', '0');
INSERT INTO `stationmap` VALUES ('10', '11', 'UNLOAD', '0');
