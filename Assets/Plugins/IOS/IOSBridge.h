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

@interface APLViewController : UIViewController <UINavigationControllerDelegate, UIImagePickerControllerDelegate>{			//interface for the main view controller for the application.

    UIImagePickerController *imagePickerController;																			//we call this object to manage photolibrary. Eventually we will need to delegate it
@public
    const char *callback_game_object_name;																					//the name of the gameobject we will be passing the NSURL to?
    const char *callback_function_name;																					//once we have passed the NSURL to a game object in the c# script, we pass the NSURL to a function!
}

@end
