#import <UIKit/UIKit.h>									
#import <MobileCoreServices/MobileCoreServices.h>

char image_url_path[1024];

@implementation NonRotatingUIImagePickerController								//implementation of the above interface - I believe it asserts that we want landscape-left and landscape-right orientation.
- (NSUInteger)supportedInterfaceOrientations
{
    return UIInterfaceOrientationMaskLandscape;
}
@end

@implementation APLViewController


- (void)viewDidLoad
{
    [super viewDidLoad];

    [self showImagePickerForSourceType:UIImagePickerControllerSourceTypePhotoLibrary];
}


- (void)showImagePickerForSourceType:(UIImagePickerControllerSourceType)sourceType
{
    imagePickerController = [[UIImagePickerController alloc] init];	
    imagePickerController.modalPresentationStyle = UIModalPresentationCurrentContext;
    imagePickerController.sourceType = sourceType;							
    
    imagePickerController.delegate = self;																													
    
    [self.view addSubview:imagePickerController.view];
}

#pragma mark - UIImagePickerControllerDelegate		

- (void)imagePickerController:(UIImagePickerController *)picker didFinishPickingMediaWithInfo:(NSDictionary *)info
{    
    NSString *mediaType = [info objectForKey:UIImagePickerControllerMediaType];

	if([mediaType isEqualToString:(NSString *)kUTTypeImage])
	{
		NSURL *urlimage = [info objectForKey:UIImagePickerControllerImageURL];
		NSLog(@"%@", urlimage); //display on console
		NSString *urlString = [urlimage absoluteString];
		const char* cp = [urlString UTF8String];			
        strcpy(image_url_path, cp);				
	}

    [self dismissViewControllerAnimated:YES completion:NULL];
    
    // UnitySendMessage("GameObject", "ImagePicked", image_url_path);
    UnitySendMessage(callback_game_object_name, callback_function_name, image_url_path);
}


- (void)imagePickerControllerDidCancel:(UIImagePickerController *)picker
{
    [self dismissViewControllerAnimated:YES completion:NULL];
}

@end

//the wrapper prevents name-mangling issues. Compile as a C function.
extern "C" {

    void OpenImagePicker(const char *game_object_name, const char *function_name) 
    {       
        if ([UIImagePickerController isSourceTypeAvailable:UIImagePickerControllerSourceTypePhotoLibrary]) 
        {
            UIViewController* parent = UnityGetGLViewController();				//grab the iPhone view that Unity app is using*
            APLViewController *uvc = [[APLViewController alloc] init];
            uvc->callback_game_object_name = strdup(game_object_name);
            uvc->callback_function_name = strdup(function_name);
            [parent presentViewController:uvc animated:YES completion:nil];
        }
    }
}
