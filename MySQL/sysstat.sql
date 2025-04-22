/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50162
Source Host           : localhost:3306
Source Database       : autoflow

Target Server Type    : MYSQL
Target Server Version : 50162
File Encoding         : 65001

Date: 2017-12-03 09:57:12
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `sysstat`
-- ----------------------------
DROP TABLE IF EXISTS `sysstat`;
CREATE TABLE `sysstat` (
  `idNo` int(11) NOT NULL DEFAULT '0',
  `runStat` int(11) DEFAULT '0' COMMENT '<运行标志>0:无效 1:运行 2:暂停 3:退出 4:更新 ',
  `autoProg` int(11) DEFAULT '0' COMMENT '自动更新程序',
  `progPath` varchar(100) DEFAULT NULL COMMENT '程序更新路径',
  PRIMARY KEY (`idNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sysstat
-- ----------------------------
INSERT INTO `sysstat` VALUES ('0', '0', '0', null);
