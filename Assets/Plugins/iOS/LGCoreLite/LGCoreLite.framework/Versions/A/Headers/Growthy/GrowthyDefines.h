//
//  GrowthyDefines.h
//  Growthy
//
//  Created by Zheng Chengxiang on 2015/10/20.
//  Copyright © 2015年 LINE. All rights reserved.
//

#ifndef GrowthyDefines_h
#define GrowthyDefines_h

static  NSString * __nonnull const kGrowthyModuleVersion  = @"1.1.2";

/**
 * @~korean
 * @brief 로그 레벨 정의
 *
 * @~japanese
 * @brief ログレバル定義
 *
 * @~english
 * @brief Log level
 */
typedef enum {
    kGrowthyDebugLevelNone,     /**< None */
    kGrowthyDebugLevelLow,      /**< Error log */
    kGrowthyDebugLevelMiddle,   /**< Error/Info log */
    kGrowthyDebugLevelHigh,     /**< Error/Info/Debug log */
} GrowthyDebugLevel;

/**
 * @~korean
 * @brief 로그인 타입 정의
 *
 * @~japanese
 * @brief ログインタイプ定義
 *
 * @~english
 * @brief Login type
 */
typedef enum {
    kLineLogin   = 0,   /**< LINE Login */
    kGuestLogin,        /**< Guest Login */
    kOtherLogin,        /**< Other Login */
    kBeforeLogin,       /**< Status before login */
    kFacebookLogin,     /**< Facebook login */
    kNaverLogin,        /**< Naver login */
    kGameCenterLogin,   /**< iOS Game Center login */
    kGoogleLogin,       /**< Google login */
} LoginType;

/**
 * @~korean
 * @brief Growthy Server 접속 환경
 *
 * @~japanese
 * @brief Growthy Serverの接続環境
 *
 * @~english
 * @brief Connection environmant fo Growthy server
 */
typedef enum {
    kGrowthyPhaseSandbox,   /**< Sandbox */
    kGrowthyPhaseAlpha,     /**< Alpha (QA) */
    kGrowthyPhaseReal       /**< Real */
} GrowthyPhase;

#endif /* GrowthyDefines_h */
