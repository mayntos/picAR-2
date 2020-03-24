//
//  IOSBridge.h
//  
//
//  Created by Maynard Santos on 3/17/20.
//

#import <Foundation/Foundation.h>

@interface IOSBridge : NSObject

@end

@interface NonRotatingUIImagePickerController : UIImagePickerController

@end

@interface APLViewController : UIViewController <UINavigationControllerDelegate, UIImagePickerControllerDelegate>
{
    UIImagePickerController *imagePickerController;																			
@public
    const char *callback_game_object_name;
    const char *callback_function_name;
}

@end
