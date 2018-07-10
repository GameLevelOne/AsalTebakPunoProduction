//
//  GrowthyManager.h
//  Growthy SDK
//
//  Created by Toru Omura on 2014/06/17.
//  Copyright (c) 2014年 LINE. All rights reserved.
//

/**
 * @file GrowthyManager.h
 * @date 2014/06/17
 * @author Copyright (c) 2014年 LINE. All rights reserved.
 * @~korean
 * @brief LINE Game의 지표 취득 기능을 제공합니다.
 *
 * @~japanese
 * @brief LINE Gameの指標取得機能を提供します。
 *
 * @~english
 * @brief The SDK provids index collecting functions.
 */

#import <Foundation/Foundation.h>
#import "GrowthyDefines.h"

/**
 * @class GrowthyManager
 * @~korean
 * Growthy SDK의 메인 클래스
 *
 * @~japanese
 * Growthy SDKのメインクラス
 *
 * @~english
 * The main class of Growthy SDK
 */
@interface GrowthyManager : NSObject

/**
 * @~korean
 * @brief 클래스의 instance를 취득합니다.
 * @return GrowthyManager의 instance
 *
 * @~japanese
 * @brief このクラスのインスタンスを取得します。
 * @return GrowthyManagerのインスタンス
 *
 * @~english
 * @brief Get the instance of this class.
 * @return The instance of GrowthyManager.
 */
+ (GrowthyManager* __nonnull) sharedManager;


/**
 * @~korean
 * @brief 클래스의 instance를 취득합니다.
 * @param userID 게임 유저의 고유ID
 * @param loginType 게임 유저의 로그인 타입 (정수)
 * @return GrowthyManager의 instance
 *
 * @~japanese
 * @brief このクラスのインスタンスを取得します。
 * @param userID ゲームプレイヤーのユーザーID
 * @param loginType ゲームプレイヤーのログインタイプ(定数)
 * @return GrowthyManagerのインスタンス
 *
 * @~english
 * @brief Get the instance of this class.
 * @param userID The user ID of the game player
 * @param loginType The login type of the game player(definition)
 * @return The instance of GrowthyManager.
 */
+ (GrowthyManager* __nonnull) sharedManagerWithUserID:(NSString* __nullable)userID
                                            loginType:(int)loginType;

/**
 * @~korean
 * @brief 초기화 처리. AppID와 ConfigKey를 지정해서 데이터 송신 서버의 환경(Sandbox, Real)을 설정한다.
 * @param appID 발급받은 App ID
 * @param configKey 발급받은 config key
 * @return self (자기자신의 인스턴스)
 *
 * @~japanese
 * @brief 初期化処理。AppIDとConfigKeyをセットしてデータの送信先をSandboxサーバーかRealサーバーかを切り分けます。
 * @param appID 発行されたApp ID
 * @param configKey 発行されたコンフィグキー
 * @return self (自分自身のインスタンス)
 *
 * @~english
 * @brief Initialization with AppId and ConfigKey to decide which server (Sandbox & Real) to send the logs.
 * @param aAppID The issued App ID
 * @param aConfigKey The issued Config Key
 * @return "self" (its own instance)
 */
- (nonnull instancetype) initWithAppID:(NSString* __nonnull)appID
                             configKey:(NSString* __nonnull)configKey;

/**
 * @~korean
 * @brief 초기화 처리. AppID와 Phase를 지정해서 데이터 송신 서버의 환경(Sandbox, Real)을 설정한다.
 * @param appID 발급받은 App ID
 * @param phase GrowthyPhase
 * @return self (자기자신의 인스턴스)
 *
 * @~japanese
 * @brief 初期化処理。AppIDとPhaseをセットしてデータの送信先をSandboxサーバーかRealサーバーかを切り分けます。
 * @param appID 発行されたApp ID
 * @param phase GrowthyPhase
 * @return self (自分自身のインスタンス)
 *
 * @~english
 * @brief Initialization with AppId and Phase to decide which server (Sandbox & Real) to send the logs.
 * @param aAppID The issued App ID
 * @param phase GrowthyPhase
 * @return "self" (its own instance)
 */
- (nonnull instancetype) initWithAppID:(NSString* __nonnull)appID
                                 phase:(GrowthyPhase)phase;

@property (nonatomic, readonly, getter = isInitialized) BOOL initialized;

/**
 * @~korean
 * @brief Offline mode. Default is NO.
 *
 * @~japanese
 * @brief Offline mode. Default is NO.
 *
 * @~english
 * @brief Offline mode. Default is NO.
 */
@property (nonatomic, assign, getter=isOfflineMode) BOOL offlineMode;

/**
 * @~korean
 * @brief User State. Default is 0.
 *
 * @~japanese
 * @brief User State. Default is 0.
 *
 * @~english
 * @brief User State. Default is 0.
 */
@property (nonatomic, assign) int userState;

/**
 * @~korean
 * @brief 지표 데이터 송신을 개시한다. (Heart Beat, Play Time, New Account Data)
 *
 * @~japanese
 * @brief 指標データ送信を開始します。(ハートビート、プレイタイム、新規アカウント)
 *
 * @~english
 * @brief Start sending data(Heart Beat, Play Time, New Account Data)
 */
- (void) start;

- (void) stop;

@property (nonatomic, assign, readonly, getter = isStartedLogging) BOOL startedLogging;

/**
 * @~korean
 * @brief 프로필 정보를 송신합니다.
 * @param jsonProfile 프로필 데이터 (json NSString)
 * @param isUpdate 업데이트 옵션
 *
 * @~japanese
 * @brief プロフィール情報を送信します。
 * @param jsonProfile プロフィールデータ(json NSString)
 * @param isUpdate 更新オプション
 *
 * @~english
 * @brief Send a profile to the GWC servers. The developer should ensure that the string passed here is in JSON form. It will be sent as an HTTP request.
 * @param jsonProfile Profile data json as NSString
 * @param isUpdate a renew option
 */
- (void) sendProfile:(NSString* __nonnull)jsonProfile isUpdate:(BOOL)isUpdate;

/**
 * @~korean
 * @brief 프로필 정보를 송신합니다.
 * @param jsonProfile 프로필 데이터 (NSDictionary)
 * @param isUpdate 업데이트 옵션
 *
 * @~japanese
 * @brief プロフィール情報を送信します。
 * @param jsonProfile プロフィールデータ(NSDictionary)
 * @param isUpdate 更新オプション
 *
 * @~english
 * @brief Send a profile to the GWC servers. The developer should ensure that the string passed here is in JSON form. It will be sent as an HTTP request.
 * @param jsonProfile Profile data NSDictionary
 * @param isUpdate a renew option
 */
- (void) sendProfileDict:(NSDictionary* __nonnull)jsonProfile isUpdate:(BOOL)isUpdate;

/**
 * @~korean
 * @brief 게임측에서 정의한 값을 이벤트 queue에 추가합니다.
 * @param jsonEvent オリジナルイベントjson NSString
 * @param name オリジナルイベント名
 *
 * @~japanese
 * @brief 開発者が独自で定義したイベントをキューに詰めます。
 * @param jsonEvent オリジナルイベントjson NSString
 * @param name オリジナルイベント名
 *
 * @~english
 * @brief Accept any events in form of JSON. They are stored in a queue. They are sent as HTTP request to the GWC servers when flushEvents is called.
 * @param jsonEvent Original event data json as NSString
 * @param name Original event name
 */
- (void) trackCustomEvent:(NSString* __nonnull)jsonEvent withName:(NSString* __nonnull)name;

/**
 * @~korean
 * @brief 게임측에서 정의한 값을 이벤트 queue에 추가합니다.
 * @param jsonEvent オリジナルイベントNSDictionary
 * @param name オリジナルイベント名
 *
 * @~japanese
 * @brief 開発者が独自で定義したイベントをキューに詰めます。
 * @param jsonEvent オリジナルイベントNSDictionary
 * @param name オリジナルイベント名
 *
 * @~english
 * @brief Accept any events in form of JSON. They are stored in a queue. They are sent as HTTP request to the GWC servers when flushEvents is called.
 * @param jsonEvent Original event data json as NSDictionary
 * @param name Original event name
 */
- (void) trackCustomEventDict:(NSDictionary* __nonnull)jsonEvent withName:(NSString* __nonnull)name;

/**
 * @~korean
 * @brief queue에 쌓인 이벤트 값을 서버에 송신합니다.
 *
 * @~japanese
 * @brief キューに貯まった独自定義イベントをサーバーへ送信します。
 *
 * @~english
 * @brief Send all events in the event queue as HTTP request to the relevant servers.
 */
- (void) flushCustomEvents;

/**
 * @~korean
 * @brief Sequential이벤트를 queue에 추가합니다.
 * @param seqValue Sequential이벤트의 문자열
 * @param seqName Sequential명
 *
 * @~japanese
 * @brief シーケンシャルイベントをキューに詰めます。
 * @param seqValue シーケンシャルイベント文字列
 * @param seqName シーケンシャル名
 *
 * @~english
 * @brief Accept a sequential event name and value. It will be packed as JSON data and sent as HTTP request to the GWC servers.
 * @param seqValue Sequential data as String
 * @param seqName Sequential name
 */
- (void) trackSequentialEvent:(NSString* __nonnull)seqValue withName:(NSString* __nonnull)seqName;

/**
 * @~korean
 * @brief Sequential이벤트를 queue에 추가합니다.
 * @param seqValue Sequential이벤트의 문자열
 * @param seqName Sequential명
 * @param extra The extra data of the sequential event as a key-value flat string map representation.
 *
 * @~japanese
 * @brief シーケンシャルイベントをキューに詰めます。
 * @param seqValue シーケンシャルイベント文字列
 * @param seqName シーケンシャル名
 * @param extra 追加情報
 *
 * @~english
 * @brief Accept a sequential event name and value. It will be packed as JSON data and sent as HTTP request to the GWC servers.
 * @param seqValue Sequential data as String
 * @param seqName Sequential name
 * @param extra The extra data of the sequential event as a key-value flat string map representation.
 */
- (void) trackSequentialEvent:(NSString* __nonnull)seqValue withName:(NSString* __nonnull)seqName withExtra:(NSDictionary* __nullable)extra;

/**
 * @~korean
 * @brief queue에 쌓인 Sequential이벤트를 서버에 송신합니다.
 *
 * @~japanese
 * @brief キューに貯まったシーケンシャルイベントをサーバーへ送信します。
 *
 * @~english
 * @brief Send all events in the sequential event queue as HTTP request to the GWC servers.
 */
- (void) flushSequentialEvents;

/**
 * @~korean
 * @brief 선물 송신 이벤트를 queue에 추가합니다.
 * @param content 콘텐츠 명
 * @param toMid 선물 받을 유저ID
 * @param tags 태그
 *
 * @~japanese
 * @brief プレゼント送信イベントをキューに詰めます。
 * @param content コンテンツ名
 * @param toMid 送信先MID
 * @param tags タグ
 *
 * @~english
 * @brief Track present sent events. It will be packed as JSON data and stored in a queue.
 *        They will be sent as HTTP request to the GWC servers when the flushPresentSentEvents is called.
 * @param content Contant name
 * @param toMid The MID that the user will send a present to
 * @param tags Tag
 */
- (void) trackPresentSentEvent:(NSString* __nonnull)content
                         toMid:(NSString* __nonnull)toMid
                      withTags:(NSString* __nullable)tags;

/**
 * @~korean
 * @brief queue에 쌓인 선물 송신 이벤트를 서버에 송신합니다.
 *
 * @~japanese
 * @brief キューに貯まったプレゼント送信イベントをサーバーへ送信します。
 *
 * @~english
 * @brief Send all events in the present sent event queue as HTTP request to the GWC servers.
 */
- (void) flushPresentSentEvents;

/**
 * @~korean
 * @brief 유저가 보낸 선물 수신 이벤트를 queue에 추가합니다.
 * @param content 콘텐츠 명
 * @param fromMid 선물 보낸 유저ID
 * @param tags 태그
 *
 * @~japanese
 * @brief ユーザーからのプレゼント受信イベントをキューに詰めます。
 * @param content コンテンツ名
 * @param fromMid 送信元MID
 * @param tags タグ
 *
 * @~english
 * @brief Track present received from user events. It will be converted as JSON data and stored in a queue.
 *        They will be sent as HTTP request to the GWC servers when the flushPresentReceivedEvents is called.
 * @param content Contant name
 * @param fromMid The MID that the user will recieve a present from
 * @param tags Tag
 */
- (void) trackPresentReceivedEventFromUser:(NSString* __nonnull)content
                                   fromMid:(NSString* __nonnull)fromMid
                                  withTags:(NSString* __nullable)tags;

/**
 * @~korean
 * @brief 운영자가 보낸 선물 수신 이벤트를 queue에 추가합니다.
 * @param content 콘텐츠 명
 * @param tags 태그
 *
 * @~japanese
 * @brief 運営からのプレゼント受信イベントをキューに詰めます。
 * @param content コンテンツ名
 * @param tags タグ
 *
 * @~english
 * @brief Track present received from admin events. It will be converted as JSON data and stored in a queue.
 *        They will be sent as HTTP request to the GWC servers when the flushPresentReceivedEvents is called.
 * @param content Content name
 * @param tags Tag
 */
- (void) trackPresentReceivedEventFromAdmin:(NSString* __nonnull)content
                                   withTags:(NSString* __nullable)tags;

/**
 * @~korean
 * @brief 큐에 쌓인 선물 수신 이벤트를 서버에 송신합니다.
 *
 * @~japanese
 * @brief キューに貯まったプレゼント受信イベントをサーバーへ送信します。
 *
 * @~english
 * @brief Send all events in the present received event queue as HTTP request to the GWC servers.
 */
- (void) flushPresentReceivedEvents;

/**
 * @brief 전체 queue에 쌓인 이벤트를 서버에 송신합니다.
 *
 * @~japanese
 * @brief 全てのキューに貯まったイベントをサーバーへ送信します。
 *
 * @~english
 * @brief Send all events in ALL queues as HTTP request to the GWC servers.
 */
- (void) flushAllEvents;

@end


#pragma mark - Static Helpers -

@interface GrowthyManager (StaticHelpers)

/**
 * @~korean
 * @brief 출력할 디버그 로그 레벨를 설정한다.
 * @param debugLevel 디버그 레벨
 *
 * @~japanese
 * @brief 出力するデバックログレベルを設定する
 * @param debugLevel デバッグレベル
 *
 * @~english
 * @brief Set the debug log level
 * @param debugLevel The debug level
 */
+ (void) setDebugLevel:(GrowthyDebugLevel)debugLevel;

/**
 * @~korean
 * @brief 런처 Uri을 저장한다.
 * @param uri 런처 Uri
 *
 * @~japanese
 * @brief ランチャーUriを保存する
 * @param uri ランチャーUri
 *
 * @~english
 * @brief Save the launch uri
 * @param uri a launch uri
 */
+ (void) saveLaunchUri:(NSURL* __nonnull)uri;

/**
 * @~korean
 * @brief 런처 Uri Paramater을 저장한다.
 * @param uri 런처 paramater dict
 *
 * @~japanese
 * @brief ランチャーUri パラメーターを保存する
 * @param uri ランチャーパラメーター
 *
 * @~english
 * @brief Save the launch uri parameter
 * @param uri a launch uri
 */
+ (void) saveLaunchUriQueryDict:(NSDictionary* __nullable)queryDict;

@end


#pragma mark - Client Info -

@protocol GrowthyClientInfo <NSObject>

typedef NS_ENUM(int8_t, GrowthyPlatformType)
{
    GrowthyPlatformTypeIos          = 1,
    GrowthyPlatformTypeAndroid      = 2,
    GrowthyPlatformTypeWindowsPhone = 3
};

typedef NS_ENUM(int8_t, GrowthyNetworkStatus)
{
    GrowthyNetworkStatusOffline = 0,
    GrowthyNetworkStatusWlan    = 1,
    GrowthyNetworkStatusWifi    = 2
};

@required
@property (nonatomic, readonly, nonnull)  NSString*              sdkVersion;
@property (nonatomic, readonly, nullable) NSString*              applicationIdentifier;
@property (nonatomic, readonly, nullable) NSString*              applicationVersion;
@property (nonatomic, readonly)           GrowthyPlatformType    platformType;
@property (nonatomic, readonly, nonnull)  NSString*              platformVersion;
@property (nonatomic, readonly, nullable) NSString*              terminalIdentifier;
@property (nonatomic, readonly, nullable) NSString*              deviceName;
@property (nonatomic, readonly, nonnull)  NSString*              countryCode;
@property (nonatomic, readonly, nonnull)  NSString*              languageCode;
@property (nonatomic, readonly)           GrowthyNetworkStatus   networkStatus;
@property (nonatomic, readonly)           LoginType              loginType;
@property (nonatomic, readonly, nullable) NSString*              mid;
@property (nonatomic, readonly, nonnull)  NSString*              mobileCountryCode;
@property (nonatomic, readonly, nonnull)  NSString*              mobileNetworkCode;
@property (nonatomic, readonly, nonnull)  NSString*              marketCode;
@property (nonatomic, readonly, nonnull)  NSString*              clientTimestamp;
@property (nonatomic, readonly, nonnull)  NSString*              lineGameSdkVersion;

//@property (nonatomic, readonly, nullable) NSString*              clientIpAddress;

@end


@interface GrowthyManager (ClientInfo)

- (id<GrowthyClientInfo> __nonnull) getClientInfo;

@end


