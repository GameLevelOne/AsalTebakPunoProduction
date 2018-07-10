//
//  LGCoreLite.h
//  LGCoreLite
//
//  Created by Kim Heejun on 2014/05/07.
//  Copyright (c) 2014年 LINE. All rights reserved.
//

/**
 * @file LGCoreLite.h
 * @date 2014/05/07
 * @author Copyright (c) 2014年 LINE. All rights reserved.
 * @~korean
 * @brief LGCoreLite에서 사용할 수 있는 각종 API와 리스너들이 정의되어 있는 헤더 파일
 *
 * @~japanese
 * @brief LGCoreLiteで使える各種のAPI群とリスナー群が定義されているヘッダファイル
 *
 * @~english
 * @brief A header file that defines the various API and Listeners used in LGCoreLite.
 */

#import <Foundation/Foundation.h>

#import "LGCoreLiteModel.h"
#import "LGCoreLiteErrorCodes.h"

static NSString *const kLGCoreLiteVersion  = @"2.5.0";

/**
 * @~korean
 * @brief 초기화 실행 상태
 *
 * @~japanese
 * @brief 初期化の実行状態
 *
 * @~english
 * @brief SDK initialization status.
 */
typedef enum {
    LGInitStateSuccess,     /**< Initial success of the LGCoreLite */
    LGInitStateFail,        /**< Initial failure of the LGCoreLite */
} LGInitState;

/**
 *
 *
 * @brief Login type
 */
typedef enum
{
    LGLineLogin = 0,
    LGGuestLogin = 1,
    LGFacebookLogin = 4,
    LGNaverLogin = 5,
    LGGameCenterLogin = 6,
    LGGoogleLogin = 7
} LGLoginType;

/**
 * @~korean
 * @brief 로그인 상태
 *
 * @~japanese
 * @brief ログインの状態
 *
 * @~english
 * @brief Login Status
 */
typedef enum {
    LGLoginStateSuccess,    /**< Successful login */
    LGLoginstateFail,       /**< Failed to login */
    LGLoginStateCancel      /**< Canceled by the user */
} LGLoginState;


typedef enum {
    LGLanClear, /** Not Maintenance and Force update */
    LGLanForceUpdate,   /** Notice of force update */
    LGLanMaintenance,   /** Notice of maintenance */
    LGLanFail           /* Failed to get notice info */
} LGLanState;

typedef void (^lg_core_lite_validation_completion_t) (BOOL isValid, NSError *error);
/**
 * @~korean
 * @brief 로그인 성공/실패를 통지하는 핸들러
 * @param loginState 로그인 상태를 반환한다
 * @param resultData 성공할 경우의 인증 정보를 반환한다
 * @param error 실패할 경우의 에러 정보를 반환한다
 *
 * @~japanese
 * @brief ログインの成功/失敗を通知するハンドル
 * @param loginState ログイン状態を返す
 * @param resultData 成功した場合に認証情報を返す
 * @param error 失敗した場合にエラー情報を返す
 *
 * @~english
 * @brief Handle that contains information about whether the Login succeeded or failed.
 * @param loginState The status of the login.
 * @param resultData Contains the authorization information if the login succeeds.
 * @param error Contains the error message if the login fails.
 */
typedef void (^lg_core_lite_login_completion_t) (LGLoginState loginState, LGLineLoginResultModel* resultData, NSError* error);

/**
 * @~korean
 * @brief 자신의 프로필 취득 요청의 성공/실패를 통지하는 핸들러
 * @param isSuccess YES(성공) / NO(실패)
 * @param resultData 성공할 경우의 자신의 프로필 정보를 반환한다
 * @param error 실패할 경우의 에러 정보를 반환한다
 *
 * @~japanese
 * @brief 自分のプロフィール取得リクエストの成功/失敗を通知するハンドル
 * @param isSuccess YES（成功）/ NO（失敗）
 * @param resultData 成功した場合に自分のプロフィール情報を返す
 * @param error 失敗した場合にエラー情報を返す
 *
 * @~english
 * @brief Handle that contains information about the get-profile request.
 * @param isSuccess YES(Success) / NO(Failure)
 * @param resultData Contains the user's profile if the access is successful.
 * @param error Contains an error message if the access fails.
 */

typedef void (^lg_core_lite_profile_completion_t) (BOOL isSuccess, LGLineProfileResultModel* resultData, NSError* error);

/**
 * @~korean
 * @brief LINE 친구 리스트 취득 요청의 성공/실패를 통지하는 핸들러
 * @param isSuccess YES(성공) / NO(실패)
 * @param resultData 성공할 경우의 친구 리스트 정보를 반환한다
 * @param error 실패할 경우의 에러 정보를 반환한다
 *
 * @~japanese
 * @brief リクエストの成功/失敗を通知するハンドル
 * @param isSuccess YES（成功） / NO（失敗）
 * @param resultData 成功した場合に友達リスト情報を返す
 * @param error 失敗する場合にエラー情報を返す
 *
 * @~english
 * @brief Handle that contains information about the get-friends request.
 * @param isSuccess YES(Success) / NO(Failure)
 * @param resultData Contains a list of the user's friends if the request was successful.
 * @param error Contains an error message if the request failed.
 */
typedef void (^lg_core_lite_friends_completion_t) (BOOL isSuccess, LGLineFriendsResultModel* resultData, NSError* error);

/**
 * @~korean
 * @brief 메세지 송신 요청의 성공/실패를 통지하는 핸들러
 * @param isSuccess YES(성공) / NO(실패)
 * @param resultData 성공할 경우의 송신 결과 정보를 반환한다
 * @param error 실패할 경우의 에러 정보를 반환한다
 *
 * @~japanese
 * @brief メッセージ送信リクエストの成功/失敗を通知するハンドル
 * @param isSuccess YES（成功） / NO（失敗）
 * @param resultData 成功した場合に送信結果情報を返す
 * @param error 失敗する場合にエラー情報を返す
 *
 * @~english
 * @brief Handle that contains information about the message transmission.
 * @param isSuccess YES(Success) / NO(Failure)
 * @param resultData Contains the result of the message transmission if it was a success.
 * @param error Contains an error message if the transmission failed.
 */
typedef void (^lg_core_lite_message_completion_t) (BOOL isSuccess, LGLineSendMessageResultModel* resultData, NSError* error);

/**
 * @~korean
 * @brief 공지사항 데이터 취득 요청의 성공/실패를 통지하는 핸들러
 * @param isSuccess YES(취득 성공) / NO(취득 실패)
 * @param state 공지사항 상태
 * @param resultData 성공할 경우의 공지사항 데이터 정보를 반환한다.
 * @param error 실패할 경우의 에러 정보를 반환한다
 *
 * @~japanese
 * @brief リクエストの成功/失敗を通知するハンドル
 * @param isSuccess YES(取得成功) / NO(取得失敗)
 * @param state お知らせの状態
 * @param resultData 成功した場合にお知らせデーター情報を返す
 * @param error 失敗する場合のエラー情報を返す
 *
 * @~english
 * @brief Handle that contains information about the notification request.
 * @param isSuccess YES(Success) / NO(Failure)
 * @param state State of notice info
 * @param resultData Contains the notification data if the request was a success.
 * @param error Contains an error message if the request was a failure.
 */
typedef void (^lg_core_lite_notice_completion_t) (BOOL isSuccess, LGLanState state, LGLineNoticeResultModel* resultData, NSError* error);

/**
 * @class LGCoreLite
 * @~korean
 * @brief LGCoreLite는 LINE에서 제공하는 인증, Social graph, 메세지 송신, 치팅 어플 탐지등의 기능들을 손쉽게 적용할 수 있도록 각 API와 리스너를 제공합니다.
 *
 * @~japanese
 * @brief LGCoreLiteはLINEが提供する認証、Social graph、メッセージ送信、cheating検知などの機能を使いやすくゲームに適用できるように各API及びリスナーを提供します。
 *
 * @~english
 * @brief LGCoreLite makes LINE integration easy by providing developers with the API and listeners that are necessary to use LINE features such as LINE Login, Link Message, Social Graph features, and cheating detection.
 */
@class LineAdapter;
@class UIViewController;
@interface LGCoreLite : NSObject
{
    LineAdapter*        _lineAdapter;           /**< LineAdapter instance */
    UIViewController*   _rootViewController;    /**< RootViewController of main windows */
}

/**
 * @brief Application ID
 */
@property (nonatomic, readonly, retain) NSString* appId;

/**
 * @brief Development pahase
 */
@property (nonatomic, readonly, retain) NSString* phase;

/**
 * @~korean
 * @brief LGCoreLite모듈 내부에서 처리중에 재로그인이 필요할때에 불려진다.
 * @details 본 리스너가 불려지면 게임에서 다시 한번 로그인 함수를 호출할 필요가 있다.
 *
 * @~japanese
 * @brief LGCoreLiteモジュール内部処理中に再ログインが必要と判断した場合に呼ばれる。
 * @details 本リスナーが呼ばれるとゲーム側は再度ログイン関数を呼び出す必要がある。
 *
 * @~english
 * @brief Called within the LGCoreLite module when a re-login is necessary.
 * @details When this listener is called, you must call the login function again.
 */
/**
 * @see Examples
 * @code
 * [LGCoreLite sharedManager].onRetryLogin = ^() {
 *     NSLog(@"Retry login, please!!");
 * };
 * @endcode
 */
@property (nonatomic, copy) void (^onRetryLogin)();


#pragma mark - Static Function
/**
 * @~korean
 * @brief LGCoreLite 인스턴스를 취득한다.
 * @return LGCoreLite instance
 *
 * @~japanese
 * @brief LGCoreLiteのインスタンスを取得する
 * @return LGCoreLite instance
 *
 * @~english
 * @brief Gets an instance of LGCoreLite.
 * @return LGCoreLite instance
 */
/**
 * @see Examples
 * @code
 * LGCoreLite* lgCoreLite = [LGCoreLite sharedManager];
 * @endcode
 */
+ (instancetype)sharedManager;

/**
 * @~korean
 * @brief LGCore에 LaunchOptions정보를 설정한다
 * @param aLaunchOptions 어플리케이션의 LaunchOptions
 * @return YES (성공) / NO (실패)
 *
 * @~japanese
 * @brief LGCoreにLaunchOptions情報を設定する
 * @param aLaunchOptions アプリケージョンのLaunchOptions
 * @return YES (成功) / NO (失敗)
 *
 * @~english
 * @brief Sets the LaunchOptions in LGCore.
 * @param aLaunchOptions Application LaunchOptions
 * @return YES (Success) / NO (Failure)
 */
/**
 * @see Examples
 * @code
 * - (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
 * {
 *      .....
 *      // Set LaunchOption to LGCore
 *      [LGCore handleLaunchOptions:launchOptions];
 *      .....
 *      return YES;
 * }
 * @endcode
 */
+ (BOOL)handleLaunchOptions:(NSDictionary *)launchOptions;

/**
 * @~korean
 * @brief URL Schemes을 설정한다
 * @param aURL Custom URL Schemes정보
 * @return YES (성공) / NO (실패)
 *
 * @~japanese
 * @brief URL Schemesを設定する
 * @param aURL Custom URL Schemes情報
 * @return YES (成功) / NO (失敗)
 *
 * @~english
 * @brief Sets URL Schemes
 * @param aURL Custom URL Scheme information.
 * @return YES (Success) / NO (Failure)
 */
/**
 * @see Examples
 * @code
 * -(BOOL)application:(UIApplication *)aApplication openURL:(NSURL *)aURL sourceApplication:(NSString *)aSourceApplication annotation:(id)aAnnotation
 * {
 *      // Set openURL to LGcore
 *      return [LGCore openURL:aURL];
 * }
 * @endcode
 */
+ (BOOL)openURL:(NSURL *)url;

#pragma mark - Init
/**
 * @~korean
 * @brief LGCoreLite을 초기화한다.
 * @details LGCoreLite모듈 초기화 및 rooting/cheating 검사를 실행한다.
 * @param appId 어플리케이션 ID
 * @param timeoutInterval 네트워크 연결 타임아웃 값 (초)
 * @param rootViewController 메인 window에 설정되어 있는 RootViewController
 * @return LGInitState 초기화 상태값\n
 * - LGInitStateSuccess : 초기화 성공
 * - LGInitStateFail : 초기화 실패
 *
 * @~japanese
 * @brief LGCoreLiteを初期化する。
 * @details LGCoreLiteモジュール初期化及びrooting/cheatingチェックを行う。
 * @param appId アプリケーションID
 * @param timeoutInterval ネットワークコネクションのタイムアウト値（秒）
 * @param rootViewController メインWindowに設定されているRootViewController
 * @return LGInitState 初期化のステータス値\n
 * - LGInitStateSuccess : 初期化成功
 * - LGInitStateFail : 初期化失敗
 *
 * @~english
 * @brief Initialize LGCoreLite.
 * @details Initializes the LGCoreLite module and performs a rooting/cheating check.
 * @param appId Application ID
 * @param phase Development phase of Line service. set to REAL before release
 * @param timeoutInterval Network connection timeout (In seconds)
 * @param rootViewController The RootViewController set in the main window.
 * @return LGInitState Result of the initialization\n
 * - LGInitStateSuccess : Success
 * - LGInitStateFail : Failure
 */
/**
 * @see Examples
 * @code
 * NSString* appId = @"LGSDKTEST";
 * int timeout = 15;
 * UIViewController* rootVC = self;     // If it want to initialize in RootViewController
 *
 * LGInitState initState = [[LGCoreLite sharedManager] initWithAppId:appId phase:@"SANDBOX" timeoutInterval:timeout rootViewController:rootVC];
 * switch (initState) {
 *     case LGInitStateFail:
 *         NSLog(@"LGCoreLite Init Fail!!!");
 *         return;
 *     case LGInitStateSuccess:
 *     default:
 *         NSLog(@"LGCoreLite Init Success!!!");
 *         break;
 * }
 * @endcode
 */
- (LGInitState)initWithAppId:(NSString *)appId
                       phase:(NSString *)phase
             timeoutInterval:(int)timeoutInterval
          rootViewController:(UIViewController *)rootViewController;

#pragma mark - IsAuthValid
/**
 * @~english
 * brief LINE isAuthValid
 * @details Check if the authentication is valid.
 *
 * @param completionHandler Returns isValid or not.
 */
/**
 * @see Examples
 * @code
 * [[LGCoreLite sharedManager] isAuthValidWithCompletionHandler:^(BOOL isValid, NSError *error) {
 *     if (isSuccess) {
 *         NSLog(@"LGCoreLite isAuthValid valid!!!");
 *     } else {
 *         if (error) {
 *             NSLog([NSString stringWithFormat:@"[ERROR] code = %d", error.code]);
 *             NSLog([NSString stringWithFormat:@"[ERROR] message = %@", error.localizedDescription]);
 *         }
 *     }
 * }];
 * @endcode
 */
- (void)isAuthValidWithCompletionHandler:(lg_core_lite_validation_completion_t)completionHandler;
#pragma mark - Login
/**
 * @~korean
 * @brief LINE 로그인 처리
 * @details 
 * - LINE App이 인스톨 되어 있으면 LINE App을 이용해서 로그인 처리를 한다.
 * - LINE App이 인스톨 되어 있지 않으면 Email로그인 처리를 한다. (Email로그인 화면은 SDK내부에서 표시함)
 * 
 * @param completionHandler 로그인 결과값을 반환한다.
 *
 * @~japanese
 * @brief LINEログイン処理
 * @details
 * - LINE Appがインストールされている場合にはLINE Appを利用してログイン処理を行う。
 * - LINE Appがインストールされていない場にはEmailログイン処理を行う。（Emailログイン画面はSDK内部で表示させる）
 * 
 * @param completionHandler ログイン結果を返す。
 *
 * @~english
 * @brief LINE Login
 * @details
 * - If the LINE App is installed, this function uses the LINE App to login.
 * - If the LINE App is not installed, this function performs the operations necessary for Email Login. (The Email Login screen display logic is contained within the SDK.
 * 
 * @param loginType Login type
 * @param appUserID Application given user ID which used when login type is guest login.
 * @param completionHandler The login result.
 */
/**
 * @see Examples
 * @code
 * [[LGCoreLite sharedManager]  loginWithCompletionHandler:LGGuestLogin appUserID:@"t12345" 
 *      completionHandler:^(LGLoginState loginState, LGLineLoginResultModel *resultData, NSError *error)
 * {
 *     switch (loginState) {
 *         case LGLoginStateCancel:
 *         {
 *             NSLog(@"LGCoreLite Login User Cancel!!!");
 *         }
 *             break;
 *         case LGLoginstateFail:
 *         {
 *             if (error) {
 *                 NSLog([NSString stringWithFormat:@"[ERROR] code = %d", error.code]);
 *                 NSLog([NSString stringWithFormat:@"[ERROR] message = %@", error.localizedDescription]);
 *             }
 *         }
 *             break;
 *         case LGLoginStateSuccess:
 *         {
 *             if (resultData) {
 *                 NSLog([NSString stringWithFormat:@"Login Info = %@", [resultData description]]);
 *             }
 *         }
 *             break;
 *         default:
 *             break;
 *     }
 * }];
 * @endcode
 */
- (void)loginWithCompletionHandler:(LGLoginType)loginType appUserID:(NSString*)appUserID completionHandler:(lg_core_lite_login_completion_t)completionHandler;

/**
 * @~korean
 * @brief 인증 정보를 삭제한다.
 * @details 인증 정보를 삭제하므로 다시 로그인할 필요가 있다.\n
 * 라인 로그인 유저의 경우에는 인증 정보를 삭제하더라도 다시 로그인하면 유저ID는 변함없이 반환되지만 access token은 갱신되어 반환된다.
 *
 * @~japanese
 * @brief 認証情報を削除する
 * @details 認証情報を削除するので、再度ログインする必要がある。\n
 * LINEログインユーザーの場合には認証情報を削除して再度ログインしてもユーザーIDの変更はないが、access tokenは更新される。
 *
 * @~english
 * @brief Deletes the authorization information.
 * @details Since this function deletes login information, it is necessary to login again after calling it.\n
 * If you delete the login information for a LINE Login user and login again afterwards, the log in information is preserved, but the access token is updated.
 */
/**
 * @see Examples
 * @code
 * [[LGCoreLite sharedManager] deleteAuthInfo];
 * @endcode
 */
- (void)deleteAuthInfo;

#pragma mark - Social graph
/**
 * @~korean
 * @brief 자신의 프로필 정보를 취득한다.
 * @param completionHandler 프로필 취득 결과값을 반환한다.
 *
 * @~japanese
 * @brief 自分のプロフィール情報を取得する。
 * @param completionHandler プロフィール取得結果を返す。
 *
 * @~english
 * @brief Gets the current user's profile.
 * @param completionHandler Returns the result of the profile request.
 */
/**
 * @see Examples
 * @code
 * [[LGCoreLite sharedManager] getProfileWithCompletionHandler:^(BOOL isSuccess, LGLineProfileResultModel *resultData, NSError *error)
 * {
 *     if (isSuccess) {
 *         if (resultData) {
 *             NSLog([NSString stringWithFormat:@"Profile Info = %@", [resultData description]]);
 *         }
 *     } else {
 *         if (error) {
 *             NSLog([NSString stringWithFormat:@"[ERROR] code = %d", error.code]);
 *             NSLog([NSString stringWithFormat:@"[ERROR] message = %@", error.localizedDescription]);
 *         }
 *     }
 * }];
 * @endcode
 */
- (void)getProfileWithCompletionHandler:(lg_core_lite_profile_completion_t)completionHandler;

- (void)getMyNonGameFriends:(lg_core_lite_friends_completion_t)completionHandler;

/**
 * @~korean
 * @brief LINE 친구 리스트 정보를 취득한다.
 * @param startIndex 취득 시점 index값 (최소값 1)
 * @param display 취득할 항목 갯수 (최소값 1, 최대값 500)
 * @param completionHandler 친구 리스트 정보 취득 결과갑을 반환한다.
 *
 * @~japanese
 * @brief LINE友達リスト情報を取得する。
 * @param startIndex 取得したいindex値 (最小値 1)
 * @param display 取得した項目の数 (最小値 1, 最大値 500)
 * @param completionHandler 友達リスト情報取得結果を返す。
 *
 * @~english
 * @brief Gets the LINE friend list data for the current user..
 * @param startIndex The index of the first element of the list you want to get. (Minimum value is 1)
 * @param display The number of elements you want to get. (Minimum value is 1, Maximum value is 500)
 * @param completionHandler The result of the request.
 */
/**
 * @see Examples
 * @code
 * // 1 ~ 20
 * [[LGCoreLite sharedManager] getFriendsWithStartIndex:1
 *                                              display:20
 *                                    completionHandler:^(BOOL isSuccess, LGLineFriendsResultModel *resultData, NSError *error)
 * {
 *     if (isSuccess) {
 *         if (resultData) {
 *             NSLog([NSString stringWithFormat:@"LINE Friend List Info = %@", [resultData description]]);
 *         }
 *     } else {
 *         if (error) {
 *             NSLog([NSString stringWithFormat:@"[ERROR] code = %d", error.code]);
 *             NSLog([NSString stringWithFormat:@"[ERROR] message = %@", error.localizedDescription]);
 *         }
 *     }
 * }];
 * @endcode
 */
- (void)getFriendsWithStartIndex:(NSInteger)startIndex
                         display:(NSInteger)display
               completionHandler:(lg_core_lite_friends_completion_t)completionHandler;

/**
 * @~korean
 * @brief 게임내의 친구 리스트 정보를 취득한다.
 * @param startIndex 취득 시점 index값 (최소값 1)
 * @param display 취득할 항목 갯수 (최소값 1, 최대값 500)
 * @param completionHandler 게임내의 친구 리스트 정보 취득 결과갑을 반환한다.
 *
 * @~japanese
 * @brief ゲーム内の友達リスト情報を取得する。
 * @param startIndex 取得したいindex値 (最小値 1)
 * @param display 取得した項目の数 (最小値 1, 最大値 500)
 * @param completionHandler ゲーム内の友達リスト情報取得結果を返す。
 *
 * @~english
 * @brief Gets the list of friends of the current user that are playing the current game.
 * @param startIndex The index of the first element of the list you want to get. (Minimum value is 1)
 * @param display The number of elements you want to get. (Minimum value is 1, Maximum value is 500)
 * @param completionHandler The result of the request.
 */
/**
 * @see Examples
 * @code
 * // 5 ~ 10
 * [[LGCoreLite sharedManager] getGameFriendsWithStartIndex:5
 *                                                  display:10
 *                                        completionHandler:^(BOOL isSuccess, LGLineFriendsResultModel *resultData, NSError *error)
 * {
 *     if (isSuccess) {
 *         if (resultData) {
 *             NSLog([NSString stringWithFormat:@"Game Friend List Info = %@", [resultData description]]);
 *         }
 *     } else {
 *         if (error) {
 *             NSLog([NSString stringWithFormat:@"[ERROR] code = %d", error.code]);
 *             NSLog([NSString stringWithFormat:@"[ERROR] message = %@", error.localizedDescription]);
 *         }
 *     }
 * }];
 * @endcode
 */
- (void)getGameFriendsWithStartIndex:(NSInteger)startIndex
                             display:(NSInteger)display
                   completionHandler:(lg_core_lite_friends_completion_t)completionHandler;

/**
 * @~korean
 * @brief LINE 유저 정보를 취득한다.
 * @param mids LINE 유저의 MID 리스트
 * @param completionHandler LINE 유저 정보 취득 결과를 반환한다.
 *
 * @~japanese
 * @brief LINEユーザー情報を取得する。
 * @param mids LINEユーザーのMIDのリスト
 * @param completionHandler LINEユーザー情報取得結果を返す。
 *
 * @~english
 * @brief get user infomation
 * @param mids list of line users
 * @param completionHandler return result of line users' profile.
 */
/**
 * @see Examples
 * @code
 * [[LGCoreLite sharedManager] getProfiles:[@"u12345", ...]
 *                       completionHandler:^( BOOL isSuccess, LGLineFriendsResultModel* resultData, NSError* error )
 * {
 *     if( isSuccess ) {
 *         // success handling implementation
 *         NSArray*  contacts  = resultData.contacts;
 *         NSInteger count     = resultData.count;
 *         NSInteger display   = resultData.display;
 *         NSInteger start     = resultData.start;
 *         NSInteger total     = resultData.total;
 *         // do something...
 *         for( NSDictionary* user in contacts ) {
 *             NSString* displayName   = user[@"displayName"];
 *             NSString* mid           = user[@"mid"];
 *             NSString* pictureUrl    = user[@"pictureUrl"];
 *             NSString* statusMessage = user[@"statusMessage"];
 *             // do something...
 *         }
 *     }
 *     else {
 *         // error handling implementation
 *         NSLog( @"%@", error );
 *     }
 * }];
 * @endcode
 */
- (void) getProfiles:(NSArray*)mids
    completionHandler:(lg_core_lite_friends_completion_t)completionHandler;

/**
 * @~korean
 * @brief LINE 메세지를 송신하다.
 * @param sendMessageModel 메세지 송신할 데이터 정보
 * @param completionHandler 메세지 송신 결과값을 반환한다.
 * @warning sendMessageModel에 지정하는 templateId 포맷에 맞는 메세지 데이터를 지정할 것.\n
 * 포맷은 LINE Developers Center에서 확인 가능
 *
 * @~japanese
 * @brief LINEメッセージを送信する。
 * @param sendMessageModel メッセージ送信するためのデーター情報
 * @param completionHandler メッセージ送信結果値を返す。
 * @warning sendMessageModelに指定するtemplateIdのフォーマットに合わせたメッセージデーターを指定してください。\n
 * フォーマットはLINE Developers Centerから確認可能
 *
 * @~english
 * @brief Send a LINE Message.
 * @param sendMessageModel The message data.
 * @param completionHandler The result of the message transmission
 * @warning sendMessageModel Please set the message data according to the template that corresponds to the templateId that specified in sendMessageModel.
 * Refer to the LINE Developer Center for information about the message format.
 */
/**
 * @see Examples
 * @code
 * LGLineSendMessageModel* sendMessageModel = [[LGLineSendMessageModel alloc] init];
 * [sendMessageModel setToUesrIds:@[@"send userId"...]];
 * [sendMessageModel setTemplateId:@"tp_lgsample_ivt_en"];
 * [sendMessageModel setTextMessage:@{@"title" : @"LGCoreLite Test", @"owner" : @"LINE"}];
 * [sendMessageModel setSubTextMessage:@{}];
 * [sendMessageModel setAltTextMessage:@{}];
 * [sendMessageModel setLinkTextMessage:@{}];
 *
 * [[LGCoreLite sharedManager] sendMessageWithModel:sendMessageModel completion:^(BOOL isSuccess, LGLineSendMessageResultModel *resultData, NSError *error) {
 *     if (isSuccess) {
 *         if (resultData) {
 *             NSLog([NSString stringWithFormat:@"Send Message Result Info = %@", [resultData description]]);
 *         }
 *     } else {
 *         if (error) {
 *             NSLog([NSString stringWithFormat:@"[ERROR] code = %d", error.code]);
 *             NSLog([NSString stringWithFormat:@"[ERROR] message = %@", error.localizedDescription]);
 *         }
 *     }
 * }];
 * @endcode
 */
- (void)sendMessageWithModel:(LGLineSendMessageModel *)sendMessageModel
                  completion:(lg_core_lite_message_completion_t)completionHandler;

#pragma mark - Ext

#pragma mark - Nelo2
- (void)enableCrashReport;

@end
