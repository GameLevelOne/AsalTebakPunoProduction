//
//  LGCoreLiteErrorCodes.h
//  LGCoreLite
//
//  Created by Kim Heejun on 2014/05/08.
//  Copyright (c) 2014年 LINE. All rights reserved.
//

/**
 * @file LGCoreLiteErrorCodes.h
 * @date 2014/05/08
 * @author Copyright (c) 2014年 LINE. All rights reserved.
 * @~korean
 * @brief LGCoreLite에서 사용하고 있는 에러 코드들을 정의해 놓은 헤더 파일
 *
 * @~japanese
 * @brief LGCoreLiteで使っているエラーコードが定義されているヘッダファイル
 *
 * @~english
 * @brief Definitions for the error codes that are used in LGCoreLite.
 */

#ifndef LGCoreLiteErrorCodes_h
#define LGCoreLiteErrorCodes_h

/**
 * @~korean
 * 내부 에러
 *
 * @~japanese
 * 内部エラー
 *
 * @~english
 * Internal Error
 */
#define LGCORE_LITE_ERROR_INTERNAL                      (1)

/**
 * @~korean
 * 입력 파라미터값이 올바르지 않음.
 *
 * @~japanese
 * 入力パラメータが正しくない。
 *
 * @~english
 * The input parameters are incorrect.
 */
#define LGCORE_LITE_ERROR_INVALID_INPUT_PARAM           (2)

/**
 * @~korean
 * App에서 Game Cheating 행위가 검출되었음.
 *
 * @~japanese
 * AppでGame Cheating行為が検出された。
 *
 * @~english
 * Cheating has been detected in the game.
 */
#define LGCORE_LITE_ERROR_LOGIN_DETECTED_CHEAT          (11)

/**
 * @~korean
 * 유저가 로그인을 취소했음.
 *
 * @~japanese
 * ユーザーがログインをキャンセルした。
 *
 * @~english
 * The user has cancelled the login.
 */
#define LGCORE_LITE_ERROR_LOGIN_CANCEL                  (12)

#define LGCORE_LITE_ERROR_LOGIN_WEB_AUTH_FAIL (17)

/**
 * @~korean
 * 인증값의 유효기간이 지났음.
 *
 * @~japanese
 * 認証情報の有効期限が切れた。
 *
 * @~english
 * The authorization info has expired.
 */
#define LGCORE_LITE_ERROR_EXPIRED_ACCESS_TOKEN             (21)

/**
 * @~korean
 * access token이 올바르지 않음.
 *
 * @~japanese
 * access tokenが正しくない。
 *
 * @~english
 * The access token is incorrect.
 */
#define LGCORE_LITE_ERROR_NOT_EXIST_ACCESS_TOKEN     (22)


/**
 * @~korean
 * LGCore 내부 에러
 *
 * @~japanese
 * LGCore内部エラー
 *
 * @~english
 * LGCore Internal Error
 */
//#define LGCORE_LITE_ERROR_EXPIRED_ACCESS_TOKEN          (31)
//#define LGCORE_LITE_ERROR_CHANNEL_API_FAIL              (32)

/**
 * @~korean
 * URL type이 올바르게 설정되어 있지 않음.
 *
 * @~japanese
 * URL typeが正しく設定されていない。
 *
 * @~english
 * URL The type is set incorrectly.
 */
#define LGCORE_LITE_ERROR_MISSING_CONFIGURATION             (41)

/**
 * @~korean
 * LINE App이 인스톨 되어 있지 않음.
 *
 * @~japanese
 * LINE Appがインストールされていない。
 *
 * @~english
 * The LINE App is not installed.
 */
#define LGCORE_LITE_ERROR_AUTHORIZATION_AGENT_NOT_AVAILABLE (42)

/**
 * @~korean
 * 내부 에러
 *
 * @~japanese
 * 内部エラー
 *
 * @~english
 * Internal Error
 */
#define LGCORE_LITE_ERROR_INTERNAL_INCONSISTENCY            (43)

/**
 * @~korean
 * 로그인 서버의 응답이 올바르지 않음.
 *
 * @~japanese
 * ログインサーバーのレスポンスが正しくない。
 *
 * @~english
 * The response from the login server is incorrect.
 */
#define LGCORE_LITE_ERROR_INVALID_SERVER_RESPONSE           (44)
//#define LGCORE_LITE_ERROR_AUTHORIZATION_DENIED              (45)

/**
 * @~korean
 * 인증 실패
 *
 * @~japanese
 * 認証失敗
 *
 * @~english
 * Authorization Failure
 */
#define LGCORE_LITE_ERROR_AUTHORIZATION_FAILED              (46)
/**
 * @~korean
 * 원인을 알 수 없는 에러
 *
 * @~japanese
 * 原因が分からない不明なエラー
 *
 * @~english
 * Unknown Error
 */
#define LGCORE_LITE_ERROR_UNKNOWN                       (-1)

#endif
