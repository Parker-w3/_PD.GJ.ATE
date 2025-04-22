/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50162
Source Host           : localhost:3306
Source Database       : autoflow

Target Server Type    : MYSQL
Target Server Version : 50162
File Encoding         : 65001

Date: 2017-12-03 09:57:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `testdata`
-- ----------------------------
DROP TABLE IF EXISTS `testdata`;
CREATE TABLE `testdata` (
  `idNo` bigint(20) NOT NULL AUTO_INCREMENT,
  `statId` int(11) DEFAULT '0',
  `statName` varchar(20) DEFAULT NULL,
  `slotNo` int(11) DEFAULT '0',
  `slotName` varchar(20) DEFAULT NULL,
  `idCard` varchar(20) DEFAULT NULL,
  `serialNo` varchar(20) DEFAULT NULL,
  `result` int(11) DEFAULT '0',
  `testTimes` int(11) DEFAULT '0',
  PRIMARY KEY (`idNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of testdata
-- ----------------------------
