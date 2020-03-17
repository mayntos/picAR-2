#import <UIKit/UIKit.h>									//brings in the UI classes we will need.
#import <MobileCoreServices/MobileCoreServices.h>		//allows for data communication between the unity app and iPhone?

#if UNITY_VERSION <= 434								//iPhone_View.h was removed at/around Unity 4.5. However, this check is added just in case someone uses those versions.
#import "iPhone_View.h"

#endif

char image_url_path[1024];								//the location of a resource that bridges to URL. In particular, the local image file that we select.
														//URL obejcts are the preferred way to refer to local files. 
														//URL objercts also allow for interapplication communication.

@interface NonRotatingUIImagePickerController : UIImagePickerController			//Declaration of a class which derives from UIImagePickerController. This is the public interface of the class.


@end

@implementation NonRotatingUIImagePickerController								//implementation of the above interface - I believe it asserts that we want landscape-left and landscape-right orientation.
- (NSUInteger)supportedInterfaceOrientations{
    return UIInterfaceOrientationMaskLandscape;
}
@end


//-----------------------------------------------------------------
//In Objective-C, attributes and methods cannot be mixed. 
//Attributes are all declared in the curly braces.
//Methods are declared after the closing brace, but before the @end statement.
@interface APLViewController : UIViewController <UINavigationControllerDelegate, UIImagePickerControllerDelegate>{			//interface for the main view controller for the application.

    UIImagePickerController *imagePickerController;																			//we call this object to manage photolibrary. Eventually we will need to delegate it
@public
    const char *callback_game_object_name ;																					//the name of the gameobject we will be passing the NSURL to?
    const char *callback_function_name ;																					//once we have passed the NSURL to a game object in the c# script, we pass the NSURL to a function!
}

@end


@implementation APLViewController


- (void)viewDidLoad
{
    [super viewDidLoad];

    [self showImagePickerForSourceType:UIImagePickerControllerSourceTypePhotoLibrary];

}


- (void)showImagePickerForSourceType:(UIImagePickerControllerSourceType)sourceType											//implementation of a specific method
{
    imagePickerController = [[UIImagePickerController alloc] init];															//allocation and initialization of the UIImagePickerController
    imagePickerController.modalPresentationStyle = UIModalPresentationCurrentContext;										//sets the photo display content over the controller's content.
    imagePickerController.sourceType = sourceType;																			//sets the imagepicker, according to the sourceType argument passed. 
    
    imagePickerController.delegate = self;											''										//the delegate receives notifications when the user picks an image or movie, or exits the picker interface.
																															//if you have a picker, you need a delegate.
    
    [self.view addSubview:imagePickerController.view];																		//adds a view to the end of the receiver's list of subviews.
}

//#pragma mark - [name] divides code into logical sections. 
//in xcode, a visual cue will be added to the source navigator, the succeeding methods will be
//enumerated underneath the [name].
#pragma mark - UIImagePickerControllerDelegate		

// This method is called when an image has been chosen from the library or taken from the camera.
- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info
{
    
    NSString *mediaType = [info objectForKey:UIImagePickerControllerMediaType];

	//Handle a still image picked from a photo album.
	if([mediaType isEqualToString:(NSString *)kUTTypeImage])
	{

		//retrieve the location of the image as a URL. This can be passed to an API.
		NSURL *urlimage = [info objectForKey:UIImagePickerControllerImageURL];		//store the image's URL
		NSLog(@"%@", urlimage);														//display on console
		NSString *urlString = [urlimage absoluteString];							//convert the NSURL to an NSString, so we may then convert it to an array of characters.
		const char* cp = [urlString UTF8String];									//convert NSString to a UTF8 character string that we will then copy into the image_url_path variable.
		// refactor: pass as value?
        strcpy(image_url_path, cp);				

	}

    [self dismissViewControllerAnimated:YES completion:NULL];						//close the viewer.
    
    // UnitySendMessage("GameObject", "ImagePicked", image_url_path);
    UnitySendMessage(callback_game_object_name, callback_function_name, image_url_path);
}


- (void)imagePickerControllerDidCancel:(UIImagePickerController *)picker
{
    [self dismissViewControllerAnimated:YES completion:NULL];
}


@end		//end of the implementation for APLViewController.




//the wrapper prevents name-mangling issues. Compile as a C function.
extern "C" {

	//the extern function we will be calling in C#.
    void OpenImagePicker(const char *game_object_name, const char *function_name) {
        
        
        if ([UIImagePickerController isSourceTypeAvailable:UIImagePickerControllerSourceTypePhotoLibrary]) {
            // APLViewController
			//utilizes the APLViewController class described in the interface and implementation.
            UIViewController* parent = UnityGetGLViewController();				//grab the iPhone view that Unity app is using*
            APLViewController *uvc = [[APLViewController alloc] init];			//declare and initialize an APLViewController object
            uvc->callback_game_object_name = strdup(game_object_name) ;			//duplicates the game object's name stores the value in uvc's "callback_game_object_name" field.
            uvc->callback_function_name = strdup(function_name) ;				//duplicates the function's name and stores the value in uvc's "callback_function_name" field.
            [parent presentViewController:uvc animated:YES completion:nil];		//animates uvc.
        }
    }
}
