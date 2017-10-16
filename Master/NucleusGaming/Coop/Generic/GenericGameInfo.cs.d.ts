/// <reference path="GameHookInfo.cs.d.ts" />

declare module server {
	interface GenericGameInfo {
		hook: server.GameHookInfo;
		options: any[];
		saveType: any;
		savePath: string;
		dirSymlinkExclusions: string[];
		fileSymlinkExclusions: string[];
		fileSymlinkCopyInstead: string[];
		handlerInterval: number;
		debug: boolean;
		supportsPositionin: boolean;
		symlinkExe: boolean;
		symlinkGame: boolean;
		hardcopyGame: boolean;
		supportsKeyboard: boolean;
		executableContext: string[];
		executableName: string;
		steamID: string;
		gUID: string;
		gameName: string;
		maxPlayers: number;
		maxPlayersOneMonitor: number;
		pauseBetweenStarts: number;
		dPIHandling: any;
		startArguments: string;
		binariesFolder: string;
		/** The relative path to where the games starts in */
		workingFolder: string;
		needsSteamEmulation: boolean;
		killMutex: string[];
		launcherExe: string;
		launcherTitle: string;
		play: any;
		setupSse: any;
		customSteps: any[];
		jsFileName: string;
		lockMouse: boolean;
		folder: string;
		handlerType: {
			memberType: any;
			declaringType: any;
			declaringMethod: {
				methodHandle: any;
				attributes: any;
				callingConvention: any;
				isGenericMethodDefinition: boolean;
				containsGenericParameters: boolean;
				isGenericMethod: boolean;
				isSecurityCritical: boolean;
				isSecuritySafeCritical: boolean;
				isSecurityTransparent: boolean;
				isPublic: boolean;
				isPrivate: boolean;
				isFamily: boolean;
				isAssembly: boolean;
				isFamilyAndAssembly: boolean;
				isFamilyOrAssembly: boolean;
				isStatic: boolean;
				isFinal: boolean;
				isVirtual: boolean;
				isHideBySig: boolean;
				isAbstract: boolean;
				isSpecialName: boolean;
				isConstructor: boolean;
			};
			reflectedType: any;
			structLayoutAttribute: {
				value: any;
			};
			gUID: any;
			module: {
				mDStreamVersion: number;
				fullyQualifiedName: string;
				moduleVersionId: any;
				metadataToken: number;
				scopeName: string;
				name: string;
				assembly: {
					codeBase: string;
					escapedCodeBase: string;
					fullName: string;
					entryPoint: {
						memberType: any;
						returnType: any;
						returnParameter: {
							parameterType: any;
							name: string;
							defaultValue: any;
							rawDefaultValue: any;
							position: number;
							attributes: any;
							member: {
								memberType: any;
								name: string;
								declaringType: any;
								reflectedType: any;
								metadataToken: number;
								module: any;
							};
							isIn: boolean;
							isOut: boolean;
							isLcid: boolean;
							isRetval: boolean;
							isOptional: boolean;
							metadataToken: number;
						};
						returnTypeCustomAttributes: any;
					};
					evidence: {
						locked: boolean;
						count: number;
						syncRoot: any;
						isSynchronized: boolean;
						isReadOnly: boolean;
					};
					permissionSet: {
						syncRoot: any;
						isSynchronized: boolean;
						isReadOnly: boolean;
						count: number;
					};
					isFullyTrusted: boolean;
					securityRuleSet: any;
					manifestModule: any;
					reflectionOnly: boolean;
					location: string;
					imageRuntimeVersion: string;
					globalAssemblyCache: boolean;
					hostContext: number;
					isDynamic: boolean;
				};
				moduleHandle: any;
			};
			assembly: {
				codeBase: string;
				escapedCodeBase: string;
				fullName: string;
				entryPoint: {
					memberType: any;
					returnType: any;
					returnParameter: {
						parameterType: any;
						name: string;
						defaultValue: any;
						rawDefaultValue: any;
						position: number;
						attributes: any;
						member: {
							memberType: any;
							name: string;
							declaringType: any;
							reflectedType: any;
							metadataToken: number;
							module: {
								mDStreamVersion: number;
								fullyQualifiedName: string;
								moduleVersionId: any;
								metadataToken: number;
								scopeName: string;
								name: string;
								assembly: any;
								moduleHandle: any;
							};
						};
						isIn: boolean;
						isOut: boolean;
						isLcid: boolean;
						isRetval: boolean;
						isOptional: boolean;
						metadataToken: number;
					};
					returnTypeCustomAttributes: any;
				};
				evidence: {
					locked: boolean;
					count: number;
					syncRoot: any;
					isSynchronized: boolean;
					isReadOnly: boolean;
				};
				permissionSet: {
					syncRoot: any;
					isSynchronized: boolean;
					isReadOnly: boolean;
					count: number;
				};
				isFullyTrusted: boolean;
				securityRuleSet: any;
				manifestModule: {
					mDStreamVersion: number;
					fullyQualifiedName: string;
					moduleVersionId: any;
					metadataToken: number;
					scopeName: string;
					name: string;
					assembly: any;
					moduleHandle: any;
				};
				reflectionOnly: boolean;
				location: string;
				imageRuntimeVersion: string;
				globalAssemblyCache: boolean;
				hostContext: number;
				isDynamic: boolean;
			};
			typeHandle: any;
			fullName: string;
			namespace: string;
			assemblyQualifiedName: string;
			baseType: any;
			typeInitializer: {
				memberType: any;
			};
			isNested: boolean;
			attributes: any;
			genericParameterAttributes: any;
			isVisible: boolean;
			isNotPublic: boolean;
			isPublic: boolean;
			isNestedPublic: boolean;
			isNestedPrivate: boolean;
			isNestedFamily: boolean;
			isNestedAssembly: boolean;
			isNestedFamANDAssem: boolean;
			isNestedFamORAssem: boolean;
			isAutoLayout: boolean;
			isLayoutSequential: boolean;
			isExplicitLayout: boolean;
			isClass: boolean;
			isInterface: boolean;
			isValueType: boolean;
			isAbstract: boolean;
			isSealed: boolean;
			isEnum: boolean;
			isSpecialName: boolean;
			isImport: boolean;
			isSerializable: boolean;
			isAnsiClass: boolean;
			isUnicodeClass: boolean;
			isAutoClass: boolean;
			isArray: boolean;
			isGenericType: boolean;
			isGenericTypeDefinition: boolean;
			isGenericParameter: boolean;
			genericParameterPosition: number;
			containsGenericParameters: boolean;
			isByRef: boolean;
			isPointer: boolean;
			isPrimitive: boolean;
			isCOMObject: boolean;
			hasElementType: boolean;
			isContextful: boolean;
			isMarshalByRef: boolean;
			isSecurityCritical: boolean;
			isSecuritySafeCritical: boolean;
			isSecurityTransparent: boolean;
			underlyingSystemType: any;
		};
	}
}
