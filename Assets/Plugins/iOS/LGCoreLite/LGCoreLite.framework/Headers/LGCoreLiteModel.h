//
//  LGCoreLiteModel.h
//  LGCoreLite
//
//  Created by Kim Heejun on 2014/05/08.
//  Copyright (c) 2014年 LINE. All rights reserved.
//

/**
 * @file LGCoreLiteModel.h
 * @date 2014/05/08
 * @author Copyright (c) 2014年 LINE. All rights reserved.
 * @~korean
 * @brief LGCoreLite에서 사용하는 Model들을 정의해 놓은 헤더 파일
 *
 * @~japanese
 * @brief LGCoreLiteで使っているModel群が定義されているヘッダファイル
 *
 * @~english
 * @brief Definitions for the Models that are used in LGCoreLite
 */

#import <Foundation/Foundation.h>

/**
 * @class LGLineProfileResultModel
 * @~korean
 * @brief 자신의 프로필 정보
 *
 * @~japanese
 * @brief 自分のプロフィール情報
 *
 * @~english
 * @brief The current user's profile information.
 */
@interface LGLineProfileResultModel : NSObject

/**
 * @~korean
 * LINE App에 표시되고 있는 자신의 이름
 *
 * @~japanese
 * LINE Appに表示している自分の名前
 *
 * @~english
 * The current user's name as displayed in the LINE App.
 */
@property (nonatomic, retain) NSString* displayName;

/**
 * @~korean
 * 유저의 고유 ID
 *
 * @~japanese
 * ユーザーのユニークなID
 *
 * @~english
 * The current user's unique ID.
 */
@property (nonatomic, retain) NSString* mid;

/**
 * @~korean
 * LINE App에 등록되어 있는 프로필 이미지 주소
 *
 * @~japanese
 * LINE Appに登録されているプロフィールイメージのURL
 *
 * @~english
 * The profile image that is current registered with the LINE App.
 */
@property (nonatomic, retain) NSString* pictureUrl;

/**
 * @~korean
 * LINE App에 등록한 한마디 메세지
 *
 * @~japanese
 * LINE Appに書き込んだ一言メッセージ
 *
 * @~english
 * The "What's up" message that is currently displayed on the current user's LINE profile.
 */
@property (nonatomic, retain) NSString* statusMessage;

/**
 * @~korean
 * JSON형식의 문자열을 반환한다
 * @return JSON형식의 문자열
 *
 * @~japanese
 * JSON形式の文字列に変換して返す
 * @return JSON形式の文字列
 *
 * @~english
 * Convert to JSON type String
 * @return JSON type String
 */
- (NSString *)jsonString;

/**
 * @~korean
 * JSON형식의 Dictionary 오브젝트를 반환한다
 * @return JSON형식의 Dictionary 오브젝트
 *
 * @~japanese
 * JSON形式のDictionaryオブジェクトに変換して返す
 * @return JSON形式のDictionaryオブジェクト
 *
 * @~english
 * Convert to JSON type dictionary
 * @return JSON type dictionary
 */
- (NSDictionary *)jsonDictionary;
@end

/**
 * @class LGLineLoginResultModel
 * @~korean
 * @brief LINE 로그인 인증 정보
 *
 * @~japanese
 * @brief LINEログイン認証情報
 *
 * @~english
 * @brief LINE Login authorization data
 */
@interface LGLineLoginResultModel : NSObject

/**
 * @~korean
 * 유저의 고유 ID
 *
 * @~japanese
 * ユーザーのユニークなID
 *
 * @~english
 * Unique User ID
 */
@property (nonatomic, retain) NSString*                 mid;

/**
 * @~korean
 * 로그인 인증후에 발행된 AccessToken
 *
 * @~japanese
 * 認証して発行されたAccessToken
 *
 * @~english
 * AccessToken that was issued by LINE Login
 */
@property (nonatomic, retain) NSString*                 accessToken;

/**
 * @~korean
 * 자신의 프로필 정보\n
 * [LGLineProfileResultModel]을 참조
 *
 * @~japanese
 * 自分のプロフィール情報\n
 * [LGLineProfileResultModel]を参照
 *
 * @~english
 * The current user's profile information.
 * Please refer to [LGLineProfileResultModel]
 */
@property (nonatomic, retain) LGLineProfileResultModel* profile;

/**
 * @~korean
 * JSON형식의 문자열을 반환한다
 * @return JSON형식의 문자열
 *
 * @~japanese
 * JSON形式の文字列に変換して返す
 * @return JSON形式の文字列
 *
 * @~english
 * Convert to JSON type String
 * @return JSON type String
 */
- (NSString *)jsonString;

/**
 * @~korean
 * JSON형식의 Dictionary 오브젝트를 반환한다
 * @return JSON형식의 Dictionary 오브젝트
 *
 * @~japanese
 * JSON形式のDictionaryオブジェクトに変換して返す
 * @return JSON形式のDictionaryオブジェクト
 *
 * @~english
 * Convert to JSON type dictionary
 * @return JSON type dictionary
 */
- (NSDictionary *)jsonDictionary;
@end

/**
 * @class LGLineFriendsResultModel
 * @~korean
 * @brief LINE 친구 프로필 리스트 정보
 *
 * @~japanese
 * @brief LINE友達のプロフィールリスト情報
 *
 * @~english
 * @brief LINE friend profile data.
 */
@interface LGLineFriendsResultModel : NSObject

/**
 * @~korean
 * LINE 친구 프로필 리스트
 *
 * @~japanese
 * LINE友達のプロフィールリスト
 *
 * @~english
 * LINE friend profile list.
 */
@property (nonatomic, retain) NSArray*  contacts;

/**
 * @~korean
 * 취득한 항목 갯수
 *
 * @~japanese
 * 取得できた項目の数
 *
 * @~english
 * The number of elements in the list.
 */
@property (nonatomic, assign) NSInteger count;

/**
 * @~korean
 * 자신의 LINE 친구 총 갯수
 *
 * @~japanese
 * 自分のLINE友達のtotalの数
 *
 * @~english
 * The number of LINE friends for the current user.
 */
@property (nonatomic, assign) NSInteger total;

/**
 * @~korean
 * 요청한 친구 리스트의 시작 index값
 *
 * @~japanese
 * 要請した友達リストの開始indexの値
 *
 * @~english
 * The index to start the getting friend list from.
 */
@property (nonatomic, assign) NSInteger start;

/**
 * @~korean
 * 요청한 친구 리스트의 항목 개수
 *
 * @~japanese
 * 要請した友達リストの項目の数
 *
 * @~english
 * The total number of friends in the list to display.
 */
@property (nonatomic, assign) NSInteger display;

/**
 * @~korean
 * JSON형식의 문자열을 반환한다
 * @return JSON형식의 문자열
 *
 * @~japanese
 * JSON形式の文字列に変換して返す
 * @return JSON形式の文字列
 *
 * @~english
 * Convert to JSON type String
 * @return JSON type String
 */
- (NSString *)jsonString;

/**
 * @~korean
 * JSON형식의 Dictionary 오브젝트를 반환한다
 * @return JSON형식의 Dictionary 오브젝트
 *
 * @~japanese
 * JSON形式のDictionaryオブジェクトに変換して返す
 * @return JSON形式のDictionaryオブジェクト
 *
 * @~english
 * Convert to JSON type dictionary
 * @return JSON type dictionary
 */
- (NSDictionary *)jsonDictionary;
@end

/**
 * @class LGLineSendMessageModel
 * @~korean
 * @brief LINE 메세지를 송신하기 위한 필요한 정보
 *
 * @~japanese
 * @brief LINEメッセージを送信するために必要な情報
 *
 * @~english
 * @brief The data that is required to send a LINE message.
 */
@interface LGLineSendMessageModel : NSObject

/**
 * @~korean
 * 라인 메세지를 보낼 친구의 MID 리스트\n
 * 1명 이상 설정가능
 *
 * @~japanese
 * LINEメッセージを送る友達のMIDリスト\n
 * １人以上設定可能
 *
 * @~english
 * The list of MIDs for the friends that the message will be sent to.\n
 * This value must be 1 or greater.
 */
@property (nonatomic, retain) NSArray*      toUesrIds;

/**
 * @~korean
 * 라인 개발자 센터에 등록되어 있는 메세지의 Template ID
 * @warning 등록된 Template ID 포맷을 확인 후에 해당 포맷에 맞도록 설정할 것
 *
 * @~japanese
 * Line Dev Centerに登録されているメッセージのTemplate ID
 * @warning 登録されているTemplate IDフォーマットを確認してそのフォーマットに合わせて設定すること
 *
 * @~english
 * The Template ID for the desired message template. This is registered in the LINE Developer Center
 * @warning Make sure to check the corresponding Template's format and set the data according to the format.
 */
@property (nonatomic, retain) NSString*     templateId;

/**
 * @~korean
 * Template의 Main Text부분의 형식에 맞춘 메세지 정보
 *
 * @~japanese
 * TemplateのMain Text部分の形式に合わせメッセージ情報
 *
 * @~english
 * Message data that will appear in the Template's Main Text.
 */
@property (nonatomic, retain) NSDictionary* textMessage;

/**
 * @~korean
 * Template의 Sub Text부분의 형식에 맞춘 메세지 정보
 *
 * @~japanese
 * TemplateのSub Text部分の形式に合わせメッセージ情報
 *
 * @~english
 * Message data that will appear in the Template's Sub Text.
 */
@property (nonatomic, retain) NSDictionary* subTextMessage;

/**
 * @~korean
 * Template의 Alt Text부분의 형식에 맞춘 메세지 정보
 *
 * @~japanese
 * TemplateのAlt Text部分の形式に合わせメッセージ情報
 *
 * @~english
 * Message data that will appear in the Template's Alt Text.
 */
@property (nonatomic, retain) NSDictionary* altTextMessage;

/**
 * @~korean
 * Template의 Link Text부분의 형식에 맞춘 메세지 정보
 *
 * @~japanese
 * TemplateのLink Text部分の形式に合わせメッセージ情報
 *
 * @~english
 * Message data that will appear in the Template's Link Text.
 */
@property (nonatomic, retain) NSDictionary* linkTextMessage;

/**
 Key-values to specify placeholders of Android Link Uri in message template
 */
@property (nonatomic, retain) NSDictionary* aLinkUriParams;

/**
 Key-values to specify placeholders of Iphone Link Uri in message template
 */
@property (nonatomic, retain) NSDictionary* iLinkUriParams;

/**
 Key-values to specify placeholders of Web Link Uri in message template, it won't be used for native apps
 */
@property (nonatomic, retain) NSDictionary* linkUriParams;

@end

/**
 * @class LGLineSendMessageResultModel
 * @~korean
 * @brief LINE 메세지 송신 결과 정보
 *
 * @~japanese
 * @brief LINEメッセージ送信結果情報
 *
 * @~english
 * @brief The result of the LINE message transmission.
 */
@interface LGLineSendMessageResultModel : NSObject

/**
 * @~korean
 * 송신한 메세지에 부여되는 값
 *
 * @~japanese
 * 送信したメッセージに付与される値
 *
 * @~english
 * The value granting the sent message
 */
@property (nonatomic, assign) NSInteger version;

/**
 * @~korean
 * 송신한 날짜와 시간
 *
 * @~japanese
 * 送信した日付と時間
 *
 * @~english
 * Sent date and time
 */
@property (nonatomic, assign) long      timeStamp;

/**
 * @~korean
 * 전송된 메세지를 식별하는 ID
 *
 * @~japanese
 * 送信したメッセージを識別するID
 *
 * @~english
 * ID to identify the sent event.
 */
@property (nonatomic, retain) NSString* messageId;

/**
 * @~korean
 * JSON형식의 문자열을 반환한다
 * @return JSON형식의 문자열
 *
 * @~japanese
 * JSON形式の文字列に変換して返す
 * @return JSON形式の文字列
 *
 * @~english
 * Convert to JSON type String
 * @return JSON type String
 */
- (NSString *)jsonString;

/**
 * @~korean
 * JSON형식의 Dictionary 오브젝트를 반환한다
 * @return JSON형식의 Dictionary 오브젝트
 *
 * @~japanese
 * JSON形式のDictionaryオブジェクトに変換して返す
 * @return JSON形式のDictionaryオブジェクト
 *
 * @~english
 * Convert to JSON type dictionary
 * @return JSON type dictionary
 */
- (NSDictionary *)jsonDictionary;
@end

/**
 * @class LGLineNoticeResultModel
 * @~korean
 * @brief 공지사항 데이터 정보
 *
 * @~japanese
 * @brief お知らせデーター情報
 *
 * @~english
 * @brief Notification data.
 */
@interface LGLineNoticeResultModel : NSObject

/**
 * @~korean
 * LINE App Notice Admin에 등록되어 있는 공지사항 정보 리스트
 *
 * @~japanese
 * Line App Notice Admin登録されているお知らせ情報リスト
 *
 * @~english
 * The notification data that is registered in the LINE App Notice Admin system.
 */
@property (nonatomic, retain) NSArray*      notifiactionResult;

/**
 * @~korean
 * LINE App Notice Admin에 등록되어 있는 Application 정보
 *
 * @~japanese
 * Line App Notice Adminに登録されている最新のアプリ情報
 *
 * @~english
 * The Application information that is registered in the LINE App Notice Admin system.
 */
@property (nonatomic, retain) NSDictionary* appInfoResult;

/**
 * @~korean
 * Line App Notice Admin에 등록되어 있는 새로운 공지사항 갯수
 *
 * @~japanese
 * Line App Notice Adminに登録されている新しいお知らせの数
 *
 * @~english
 * The number of new notifications that are registered in the Line App Notice Admin system.
 */
@property (nonatomic, retain) NSNumber*     newCountResult;

/**
 * @~korean
 * JSON형식의 문자열을 반환한다
 * @return JSON형식의 문자열
 *
 * @~japanese
 * JSON形式の文字列に変換して返す
 * @return JSON形式の文字列
 *
 * @~english
 * Convert to JSON type String
 * @return JSON type String
 */
- (NSString *)jsonString;

/**
 * @~korean
 * JSON형식의 Dictionary 오브젝트를 반환한다
 * @return JSON형식의 Dictionary 오브젝트
 *
 * @~japanese
 * JSON形式のDictionaryオブジェクトに変換して返す
 * @return JSON形式のDictionaryオブジェクト
 *
 * @~english
 * Convert to JSON type dictionary
 * @return JSON type dictionary
 */
- (NSDictionary *)jsonDictionary;
@end
