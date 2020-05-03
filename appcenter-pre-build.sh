#!/usr/bin/env bash

echo "##[warning][Pre-Build Action] - Lets do some Pre build transformations..."

# Declare local script variables
SCRIPT_ERROR=0

# Define the files to manipulate
INFO_PLIST_FILE=${APPCENTER_SOURCE_DIRECTORY}/XamarinWeatherApp.iOS/Info.plist

echo "##[warning][Pre-Build Action] - Checking if all files and environment variables are available..."

######################## Display name Validisation
if [ -z "${APP_DISPLAY_NAME}" ]
then
    echo "##[error][Pre-Build Action] - APP_DISPLAY_NAME variable needs to be defined in App Center!!!"
    let "SCRIPT_ERROR += 1"
    else
    echo "##[warning][Pre-Build Action] - APP_DISPLAY_NAME variable - oK!"
fi

######################## Build ID Validisation
if [ -z "${APPCENTER_BUILD_ID}" ]
then
    echo "##[error][Pre-Build Action] - APPCENTER_BUILD_ID variable needs to be defined in App Center!!!"
    let "SCRIPT_ERROR += 1"
    else
    echo "##[warning][Pre-Build Action] - APPCENTER_BUILD_ID variable - oK!"
fi

######################## File Checker
if [ -e "${INFO_PLIST_FILE}" ]
then
    echo "##[warning][Pre-Build Action] - Info.plist file found - oK!"
else
    echo "##[error][Pre-Build Action] - Info.plist file not found!"
    let "SCRIPT_ERROR += 1"
fi

######################## Error Handler
if [ ${SCRIPT_ERROR} -gt 0 ]
then
    echo "##[error][Pre-Build Action] - There are ${SCRIPT_ERROR} errors."
    echo "##[error][Pre-Build Action] - Fix them and try again..."
    exit 1 # this will kill the build
    # exit # this will exit this script, but continues building
fi

echo "##[warning][Pre-Build Action] - There are ${SCRIPT_ERROR} errors."
echo "##[warning][Pre-Build Action] - Now everything is checked, lets change the app display name on iOS..."

######################## Change name on iOS 
if [ -e "$INFO_PLIST_FILE" ]
then
    echo "##[command][Pre-Build Action] - Changing the App display name on iOS to: $APP_DISPLAY_NAME "
    plutil -replace CFBundleDisplayName -string "$APP_DISPLAY_NAME" $INFO_PLIST_FILE

    echo "##[section][Pre-Build Action] - Info.plist File content:"
    cat $INFO_PLIST_FILE
    echo "##[section][Pre-Build Action] - Info.plist EOF"
fi

######################## Change BUILD_ID on iOS 
if [ -e "$APPCENTER_BUILD_ID" ]
then
    echo "##[command][Pre-Build Action] - Changing the App Build ID on iOS to: $APPCENTER_BUILD_ID "
    plutil -replace CFBundleVersion -string "$APPCENTER_BUILD_ID" $INFO_PLIST_FILE

    echo "##[section][Pre-Build Action] - Info.plist File content:"
    cat $INFO_PLIST_FILE
    echo "##[section][Pre-Build Action] - Info.plist EOF"
fi
