/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50162
Source Host           : localhost:3306
Source Database       : autoflow

Target Server Type    : MYSQL
Target Server Version : 50162
File Encoding         : 65001

Date: 2017-12-03 09:56:53
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `ponmes`
-- ----------------------------
DROP TABLE IF EXISTS `ponmes`;
CREATE TABLE `ponmes` (
  `idNo` int(11) NOT NULL DEFAULT '0',
  `connectFlag` int(11) DEFAULT '0',
  `po` varchar(50) DEFAULT NULL COMMENT '工单号',
  `lineShift` varchar(50) DEFAULT NULL COMMENT '班次',
  `user` varchar(50) DEFAULT NULL COMMENT '操作者',
  `remark0` varchar(50) DEFAULT NULL,
  `remark1` varchar(50) DEFAULT NULL,
  `remark2` varchar(50) DEFAULT NULL,
  `remark3` varchar(50) DEFAULT NULL,
  `remark4` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idNo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of ponmes
-- ----------------------------
INSERT INTO `ponmes` VALUES ('0', '1', '0123456789', 'A', 'Admin', null, null, null, null, null);
