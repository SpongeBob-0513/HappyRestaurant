/*
 Navicat MySQL Data Transfer

 Source Server         : MySql
 Source Server Type    : MySQL
 Source Server Version : 50736
 Source Host           : localhost:3306
 Source Schema         : gamedb

 Target Server Type    : MySQL
 Target Server Version : 50736
 File Encoding         : 65001

 Date: 02/06/2022 17:12:47
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for result
-- ----------------------------
DROP TABLE IF EXISTS `result`;
CREATE TABLE `result`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `userid` int(11) NOT NULL,
  `totalcount` int(11) NOT NULL DEFAULT 0,
  `maxscore` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `fk_userid`(`userid`) USING BTREE,
  CONSTRAINT `fk_userid` FOREIGN KEY (`userid`) REFERENCES `user` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of result
-- ----------------------------
INSERT INTO `result` VALUES (1, 1, 1, 14);
INSERT INTO `result` VALUES (2, 2, 1, 14);
INSERT INTO `result` VALUES (3, 7, 1, 19);
INSERT INTO `result` VALUES (4, 8, 1, 19);
INSERT INTO `result` VALUES (5, 9, 1, 19);
INSERT INTO `result` VALUES (6, 10, 1, 19);
INSERT INTO `result` VALUES (7, 11, 1, 19);
INSERT INTO `result` VALUES (8, 12, 1, 16);
INSERT INTO `result` VALUES (9, 14, 1, 36);
INSERT INTO `result` VALUES (10, 15, 1, 36);

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `password` varchar(45) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `username`(`username`) USING BTREE COMMENT 'username 唯一标识'
) ENGINE = InnoDB AUTO_INCREMENT = 16 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES (1, '1', '1');
INSERT INTO `user` VALUES (2, '2', '2');
INSERT INTO `user` VALUES (3, '3', '3');
INSERT INTO `user` VALUES (4, 'lala', '123');
INSERT INTO `user` VALUES (5, '4', '4');
INSERT INTO `user` VALUES (6, 'huhu', '1');
INSERT INTO `user` VALUES (7, '???', '1');
INSERT INTO `user` VALUES (8, '123', '123');
INSERT INTO `user` VALUES (9, '222', '222');
INSERT INTO `user` VALUES (10, '11', '11');
INSERT INTO `user` VALUES (11, '22', '22');
INSERT INTO `user` VALUES (12, '33', '33');
INSERT INTO `user` VALUES (13, '77', '77');
INSERT INTO `user` VALUES (14, '44', '44');
INSERT INTO `user` VALUES (15, '55', '55');

SET FOREIGN_KEY_CHECKS = 1;
